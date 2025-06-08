import { createI18n } from 'vue-i18n'

const messages = {
  en: {
    nav: {
      home: 'Home',
      favorites: 'Favorites',
      login: 'Login',
      register: 'Register',
      profile: 'Profile',
      logout: 'Logout'
    },
    recipe: {
      cookingTime: 'Cooking Time',
      servings: 'Servings',
      difficulty: 'Difficulty',
      ingredients: 'Ingredients',
      instructions: 'Instructions',
      step: 'Step',
      addToFavorites: 'Add to Favorites',
      copyLink: 'Copy Link',
      linkCopied: 'Link copied to clipboard',
      loginRequired: 'Please login to add recipes to favorites',
      loading: 'Loading recipe details...'
    }
  },
  uk: {
    nav: {
      home: 'Головна',
      favorites: 'Улюблені',
      login: 'Увійти',
      register: 'Реєстрація',
      profile: 'Профіль',
      logout: 'Вийти'
    },
    recipe: {
      cookingTime: 'Час приготування',
      servings: 'Порції',
      difficulty: 'Складність',
      ingredients: 'Інгредієнти',
      instructions: 'Інструкції',
      step: 'Крок',
      addToFavorites: 'Додати в улюблені',
      copyLink: 'Копіювати посилання',
      linkCopied: 'Посилання скопійовано',
      loginRequired: 'Увійдіть, щоб додавати рецепти в улюблені',
      loading: 'Завантаження рецепту...'
    }
  }
}

export const i18n = createI18n({
  legacy: false,
  locale: 'en',
  fallbackLocale: 'en',
  messages
})