<template>
  <v-card>
    <v-img
      :src="recipe.image"
      height="200"
      cover
    ></v-img>
    
    <v-card-title>{{ recipe.title }}</v-card-title>
    
    <v-card-text>
      <div class="d-flex align-center mb-2">
        <v-icon icon="mdi-clock-outline" class="mr-1"></v-icon>
        {{ recipe.cookingTime }} mins
        
        <v-icon icon="mdi-account-outline" class="ml-4 mr-1"></v-icon>
        {{ recipe.servings }} servings
      </div>
      <div class="d-flex flex-wrap gap-1 mb-2">
        <v-chip
          v-for="tag in recipe.dietaryTags"
          :key="tag"
          size="x-small"
          color="success"
          variant="outlined"
        >
          {{ formatDietaryTag(tag) }}
        </v-chip>
      </div>
      <p>{{ recipe.description }}</p>
    </v-card-text>

    <v-card-actions>
      <v-btn
        :to="'/recipe/' + recipe.id"
        color="primary"
        variant="text"
      >
        View Recipe
      </v-btn>
      <v-spacer></v-spacer>
      <v-tooltip
        :text="!authStore.isAuthenticated ? 'Please login to add to favorites' : ''"
        location="top"
      >
        <template v-slot:activator="{ props }">
          <v-btn
            icon
            :color="isFavorite ? 'red' : 'grey'"
            variant="text"
            v-bind="props"
            @click="handleFavorite"
            :disabled="!authStore.isAuthenticated"
          >
            <v-icon>{{ isFavorite ? 'mdi-heart' : 'mdi-heart-outline' }}</v-icon>
          </v-btn>
        </template>
      </v-tooltip>
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRecipeStore } from '../stores/recipes'
import { useAuthStore } from '../stores/auth'

interface Recipe {
  id: number
  title: string
  description: string
  image: string
  cookingTime: number
  servings: number
  ingredients: string[]
  dietaryTags: string[]
}

interface Props {
  recipe: Recipe
  showLoginSnackbar?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  showLoginSnackbar: false
})

const emit = defineEmits<{
  favoriteClick: [recipe: Recipe]
}>()

const recipeStore = useRecipeStore()
const authStore = useAuthStore()

const isFavorite = computed(() => {
  return recipeStore.favorites.includes(props.recipe.id)
})

const formatDietaryTag = (tag: string) => {
  const tagMap: { [key: string]: string } = {
    'vegetarian': 'Vegetarian',
    'vegan': 'Vegan',
    'glutenFree': 'Gluten Free',
    'dairyFree': 'Dairy Free',
    'keto': 'Keto',
    'lowCarb': 'Low Carb',
    'nutFree': 'Nut Free',
    'soyFree': 'Soy Free',
    'lactoseFree': 'Lactose Free',
    'paleo': 'Paleo',
    'whole30': 'Whole30'
  }
  return tagMap[tag] || tag
}

const handleFavorite = () => {
  emit('favoriteClick', props.recipe)
}
</script>