import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useRecipeStore = defineStore('recipes', () => {
  const recipes = ref([])
  const favorites = ref<number[]>([])
  const userPreferences = ref({
    dietRestrictions: [] as string[]
  })

  function setUserPreferences(preferences: any) {
    userPreferences.value = preferences
  }

  function toggleFavorite(recipeId: number) {
    const index = favorites.value.indexOf(recipeId)
    if (index > -1) {
      favorites.value.splice(index, 1)
    } else {
      favorites.value.push(recipeId)
    }
  }

  async function fetchRecommendedRecipes(filters = {}) {
    // TODO: Implement API call to fetch recommended recipes
    // This will use userPreferences and additional filters
  }

  return {
    recipes,
    favorites,
    userPreferences,
    setUserPreferences,
    toggleFavorite,
    fetchRecommendedRecipes
  }
})