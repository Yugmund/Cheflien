<template>
  <div>
    <h1 class="text-h4 mb-6">Recommended Recipes</h1>

    <!-- Filters -->
    <RecipeFilters
      v-model:selected-ingredients="selectedIngredients"
      v-model:cooking-time="cookingTime"
      @apply-filters="applyFilters"
    />

    <!-- Recipe Grid -->
    <v-row>
      <v-col v-for="recipe in paginatedRecipes" :key="recipe.id" cols="12" sm="6" md="4">
        <RecipeCard
          :recipe="recipe"
          @favorite-click="handleFavorite"
        />
      </v-col>
    </v-row>

    <!-- Pagination -->
    <div class="text-center mt-6">
      <v-pagination
        v-model="page"
        :length="totalPages"
        :total-visible="7"
      ></v-pagination>
    </div>

    <!-- Login Required Snackbar -->
    <v-snackbar
      v-model="showLoginSnackbar"
      color="info"
      timeout="3000"
    >
      Please login to add recipes to favorites
      <template v-slot:actions>
        <v-btn
          color="white"
          variant="text"
          to="/login"
        >
          Login
        </v-btn>
      </template>
    </v-snackbar>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRecipeStore } from '../stores/recipes'
import { useAuthStore } from '../stores/auth'
import { recipes } from '../data/recipes'
import RecipeCard from '../components/RecipeCard.vue'
import RecipeFilters from '../components/RecipeFilters.vue'

const recipeStore = useRecipeStore()
const authStore = useAuthStore()
const showLoginSnackbar = ref(false)

// Pagination
const page = ref(1)
const itemsPerPage = 6

// Filters
const selectedIngredients = ref<string[]>([])
const cookingTime = ref('')

// Filtered recipes based on dietary restrictions and other filters
const filteredRecipes = computed(() => {
  let filtered = [...recipes]

  // Filter by dietary restrictions
  if (authStore.isAuthenticated && recipeStore.userPreferences.dietRestrictions.length > 0) {
    filtered = filtered.filter(recipe => {
      return recipeStore.userPreferences.dietRestrictions.every(restriction => 
        recipe.dietaryTags.includes(restriction)
      )
    })
  }

  // Filter by ingredients
  if (selectedIngredients.value.length > 0) {
    filtered = filtered.filter(recipe =>
      selectedIngredients.value.every(ingredient =>
        recipe.ingredients.some(recipeIngredient => 
          recipeIngredient.toLowerCase().includes(ingredient.toLowerCase())
        )
      )
    )
  }

  // Filter by cooking time
  if (cookingTime.value) {
    const maxTime = parseInt(cookingTime.value)
    filtered = filtered.filter(recipe => recipe.cookingTime <= maxTime)
  }

  return filtered
})

// Paginated recipes
const paginatedRecipes = computed(() => {
  const start = (page.value - 1) * itemsPerPage
  const end = start + itemsPerPage
  return filteredRecipes.value.slice(start, end)
})

// Total pages
const totalPages = computed(() => {
  return Math.ceil(filteredRecipes.value.length / itemsPerPage)
})

const handleFavorite = (recipe: any) => {
  if (!authStore.isAuthenticated) {
    showLoginSnackbar.value = true
    return
  }
  recipeStore.toggleFavorite(recipe.id)
}

const applyFilters = () => {
  page.value = 1 // Reset to first page when filters change
}

onMounted(async () => {
  await recipeStore.fetchRecommendedRecipes()
})
</script>