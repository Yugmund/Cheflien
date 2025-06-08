<template>
  <div class="max-w-4xl mx-auto mt-10">
    <v-card v-if="recipe" class="pa-6">
      <v-card-title class="text-h4 mb-4">{{ recipe.title }}</v-card-title>
      
      <v-img
        :src="recipe.image"
        :alt="recipe.title"
        height="400"
        cover
        class="rounded-lg mb-6"
      ></v-img>

      <!-- Dietary Tags -->
      <div v-if="recipe.dietaryTags && recipe.dietaryTags.length > 0" class="mb-6">
        <h3 class="text-h6 mb-3">
          <v-icon icon="mdi-leaf" class="mr-2"></v-icon>
          Dietary Information
        </h3>
        <div class="d-flex flex-wrap gap-2">
          <v-chip
            v-for="tag in recipe.dietaryTags"
            :key="tag"
            color="success"
            variant="outlined"
            size="default"
          >
            <v-icon start icon="mdi-check-circle"></v-icon>
            {{ formatDietaryTag(tag) }}
          </v-chip>
        </div>
      </div>

      <v-row class="mb-6">
        <v-col cols="4">
          <v-card>
            <v-card-text class="text-center">
              <v-icon icon="mdi-clock-outline" size="large" color="primary" class="mb-2"></v-icon>
              <div class="text-body-1">{{ $t('recipe.cookingTime') }}</div>
              <div class="text-h6">{{ recipe.cookingTime }} mins</div>
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="4">
          <v-card>
            <v-card-text class="text-center">
              <v-icon icon="mdi-account-group" size="large" color="primary" class="mb-2"></v-icon>
              <div class="text-body-1">{{ $t('recipe.servings') }}</div>
              <div class="text-h6">{{ recipe.servings }}</div>
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="4">
          <v-card>
            <v-card-text class="text-center">
              <v-icon icon="mdi-chef-hat" size="large" color="primary" class="mb-2"></v-icon>
              <div class="text-body-1">{{ $t('recipe.difficulty') }}</div>
              <div class="text-h6">{{ recipe.difficulty }}</div>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>

      <v-card class="mb-6">
        <v-card-title class="text-h5">
          <v-icon icon="mdi-food-variant" class="mr-2"></v-icon>
          {{ $t('recipe.ingredients') }}
        </v-card-title>
        <v-card-text>
          <v-list>
            <v-list-item v-for="(ingredient, index) in recipe.ingredients" :key="index">
              <template v-slot:prepend>
                <v-icon icon="mdi-circle-small"></v-icon>
              </template>
              {{ ingredient }}
            </v-list-item>
          </v-list>
        </v-card-text>
      </v-card>

      <v-card>
        <v-card-title class="text-h5">
          <v-icon icon="mdi-book-open-variant" class="mr-2"></v-icon>
          {{ $t('recipe.instructions') }}
        </v-card-title>
        <v-card-text>
          <v-timeline>
            <v-timeline-item
              v-for="(step, index) in recipe.instructions"
              :key="index"
              :dot-color="'primary'"
              size="small"
            >
              <template v-slot:opposite>
                {{ $t('recipe.step') }} {{ index + 1 }}
              </template>
              <div class="text-body-1">{{ step }}</div>
            </v-timeline-item>
          </v-timeline>
        </v-card-text>
      </v-card>

      <v-card-actions class="mt-6">
        <v-btn
          :color="isFavorite ? 'red' : 'primary'"
          :prepend-icon="isFavorite ? 'mdi-heart' : 'mdi-heart-outline'"
          :disabled="!authStore.isAuthenticated"
          @click="handleFavorite"
        >
          {{ isFavorite ? 'Remove from Favorites' : $t('recipe.addToFavorites') }}
        </v-btn>
        <v-btn
          color="secondary"
          prepend-icon="mdi-content-copy"
          @click="copyRecipeLink"
        >
          {{ $t('recipe.copyLink') }}
        </v-btn>
      </v-card-actions>
    </v-card>

    <v-card v-else class="pa-6 text-center">
      <v-progress-circular
        indeterminate
        color="primary"
        size="64"
      ></v-progress-circular>
      <div class="text-h6 mt-4">{{ $t('recipe.loading') }}</div>
    </v-card>

    <!-- Snackbar -->
    <v-snackbar
      v-model="snackbar.show"
      :color="snackbar.color"
      :timeout="3000"
    >
      {{ snackbar.text }}
    </v-snackbar>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useRoute } from 'vue-router'
import { useAuthStore } from '../stores/auth'
import { useRecipeStore } from '../stores/recipes'
import { useI18n } from 'vue-i18n'
import { recipes } from '../data/recipes'

const route = useRoute()
const authStore = useAuthStore()
const recipeStore = useRecipeStore()
const { t } = useI18n()

const recipe = ref<any>(null)
const snackbar = ref({
  show: false,
  text: '',
  color: 'success'
})

const isFavorite = computed(() => {
  return recipe.value ? recipeStore.favorites.includes(recipe.value.id) : false
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

onMounted(async () => {
  const recipeId = parseInt(route.params.id as string)
  recipe.value = recipes.find(r => r.id === recipeId) || recipes[0]
})

const handleFavorite = () => {
  if (!authStore.isAuthenticated) {
    snackbar.value = {
      show: true,
      text: t('recipe.loginRequired'),
      color: 'info'
    }
    return
  }
  
  recipeStore.toggleFavorite(recipe.value.id)
  
  snackbar.value = {
    show: true,
    text: isFavorite.value ? 'Added to favorites!' : 'Removed from favorites!',
    color: 'success'
  }
}

const copyRecipeLink = async () => {
  try {
    await navigator.clipboard.writeText(window.location.href)
    snackbar.value = {
      show: true,
      text: t('recipe.linkCopied'),
      color: 'success'
    }
  } catch (err) {
    snackbar.value = {
      show: true,
      text: 'Failed to copy link',
      color: 'error'
    }
  }
}
</script>