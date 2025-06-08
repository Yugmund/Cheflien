<template>
  <div>
    <h1 class="text-h4 mb-6">{{ $t('nav.favorites') }}</h1>

    <div v-if="!authStore.isAuthenticated" class="text-center py-12">
      <v-icon icon="mdi-heart-outline" size="64" color="grey" class="mb-4"></v-icon>
      <h2 class="text-h5 mb-4">Please login to view your favorites</h2>
      <v-btn color="primary" to="/login" prepend-icon="mdi-login">
        Login
      </v-btn>
    </div>

    <div v-else-if="favoriteRecipes.length === 0" class="text-center py-12">
      <v-icon icon="mdi-heart-outline" size="64" color="grey" class="mb-4"></v-icon>
      <h2 class="text-h5 mb-4">No favorite recipes yet</h2>
      <p class="text-body-1 mb-4">Start exploring recipes and add them to your favorites!</p>
      <v-btn color="primary" to="/" prepend-icon="mdi-home">
        Browse Recipes
      </v-btn>
    </div>

    <div v-else>
      <!-- Recipe Grid -->
      <v-row>
        <v-col v-for="recipe in paginatedFavorites" :key="recipe.id" cols="12" sm="6" md="4">
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
              <v-btn
                icon
                color="red"
                variant="text"
                @click="removeFavorite(recipe.id)"
              >
                <v-icon>mdi-heart</v-icon>
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>

      <!-- Pagination -->
      <div class="text-center mt-6" v-if="totalPages > 1">
        <v-pagination
          v-model="page"
          :length="totalPages"
          :total-visible="7"
        ></v-pagination>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { useRecipeStore } from '../stores/recipes'
import { useAuthStore } from '../stores/auth'
import { recipes } from '../data/recipes'

const recipeStore = useRecipeStore()
const authStore = useAuthStore()

// Pagination
const page = ref(1)
const itemsPerPage = 6

const favoriteRecipes = computed(() => {
  return recipes.filter(recipe => recipeStore.favorites.includes(recipe.id))
})

// Paginated favorites
const paginatedFavorites = computed(() => {
  const start = (page.value - 1) * itemsPerPage
  const end = start + itemsPerPage
  return favoriteRecipes.value.slice(start, end)
})

// Total pages
const totalPages = computed(() => {
  return Math.ceil(favoriteRecipes.value.length / itemsPerPage)
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

const removeFavorite = (recipeId: number) => {
  recipeStore.toggleFavorite(recipeId)
  // Reset to first page if current page becomes empty
  if (paginatedFavorites.value.length === 0 && page.value > 1) {
    page.value = 1
  }
}
</script>