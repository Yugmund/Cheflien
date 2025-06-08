<template>
  <v-app>
    <v-app-bar>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-app-bar-title>
        <router-link to="/" class="text-decoration-none text-black">
          Cheflien
        </router-link>
      </v-app-bar-title>
      <v-spacer></v-spacer>
      <v-btn-toggle
        v-model="currentLocale"
        mandatory
        density="comfortable"
        color="primary"
      >
        <v-btn value="en">EN</v-btn>
        <v-btn value="uk">UA</v-btn>
      </v-btn-toggle>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" temporary>
      <v-list>
        <v-list-item to="/" prepend-icon="mdi-home" :title="$t('nav.home')"></v-list-item>
        <v-list-item to="/favorites" prepend-icon="mdi-heart" :title="$t('nav.favorites')"></v-list-item>
        
        <v-divider class="my-2"></v-divider>

        <template v-if="!authStore.isAuthenticated">
          <v-list-item to="/login" prepend-icon="mdi-login" :title="$t('nav.login')"></v-list-item>
          <v-list-item to="/register" prepend-icon="mdi-account-plus" :title="$t('nav.register')"></v-list-item>
        </template>
        <template v-else>
          <v-list-item to="/profile" prepend-icon="mdi-account" :title="$t('nav.profile')"></v-list-item>
          <v-list-item @click="logout" prepend-icon="mdi-logout" :title="$t('nav.logout')"></v-list-item>
        </template>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <v-container>
        <router-view></router-view>
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useAuthStore } from './stores/auth'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'

const authStore = useAuthStore()
const router = useRouter()
const { locale } = useI18n()
const drawer = ref(false)
const currentLocale = ref('en')

watch(currentLocale, (newLocale) => {
  locale.value = newLocale
})

const logout = () => {
  authStore.logout()
  router.push('/login')
  drawer.value = false
}
</script>