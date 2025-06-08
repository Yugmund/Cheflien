<template>
  <v-card class="mx-auto mt-10" max-width="500">
    <v-card-title class="text-h5 mb-4">Register</v-card-title>
    <v-card-text>
      <v-form @submit.prevent="handleRegister">
        <v-text-field
          v-model="email"
          label="Email"
          type="email"
          required
          prepend-icon="mdi-email"
        ></v-text-field>
        
        <v-text-field
          v-model="password"
          label="Password"
          type="password"
          required
          prepend-icon="mdi-lock"
        ></v-text-field>

        <v-text-field
          v-model="confirmPassword"
          label="Confirm Password"
          type="password"
          required
          prepend-icon="mdi-lock-check"
        ></v-text-field>

        <v-btn
          type="submit"
          color="primary"
          block
          class="mt-4"
          prepend-icon="mdi-account-plus"
        >
          Register
        </v-btn>

        <v-btn
          to="/login"
          color="secondary"
          block
          class="mt-2"
          variant="text"
        >
          Already have an account? Login
        </v-btn>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'

const router = useRouter()
const authStore = useAuthStore()

const email = ref('')
const password = ref('')
const confirmPassword = ref('')

const handleRegister = async () => {
  if (password.value !== confirmPassword.value) {
    alert('Passwords do not match')
    return
  }

  try {
    await authStore.register({ email: email.value, password: password.value })
    router.push('/')
  } catch (error) {
    console.error('Registration failed:', error)
  }
}
</script>