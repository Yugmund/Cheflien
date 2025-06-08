<template>
  <v-card class="mx-auto mt-6" max-width="800">
    <v-card-title class="text-h4 mb-4">
      <v-icon icon="mdi-account-cog" class="mr-2"></v-icon>
      User Profile
    </v-card-title>

    <v-card-text>
      <!-- Dietary Restrictions -->
      <v-card class="mb-6" variant="outlined">
        <v-card-title class="text-h6">
          <v-icon icon="mdi-food-apple" class="mr-2"></v-icon>
          Dietary Restrictions
        </v-card-title>
        <v-card-text>
          <v-row>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Vegetarian"
                value="vegetarian"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Vegan"
                value="vegan"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Gluten Free"
                value="glutenFree"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Dairy Free"
                value="dairyFree"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Keto"
                value="keto"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Low Carb"
                value="lowCarb"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Nut Free"
                value="nutFree"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Soy Free"
                value="soyFree"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Lactose Free"
                value="lactoseFree"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Paleo"
                value="paleo"
                color="success"
              ></v-checkbox>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-checkbox
                v-model="preferences.dietRestrictions"
                label="Whole30"
                value="whole30"
                color="success"
              ></v-checkbox>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>

      <!-- Change Password -->
      <v-card class="mb-6" variant="outlined">
        <v-card-title class="text-h6">
          <v-icon icon="mdi-lock" class="mr-2"></v-icon>
          Change Password
        </v-card-title>
        <v-card-text>
          <v-form @submit.prevent="changePassword">
            <v-text-field
              v-model="passwordForm.currentPassword"
              label="Current Password"
              type="password"
              required
              class="mb-2"
            ></v-text-field>
            <v-text-field
              v-model="passwordForm.newPassword"
              label="New Password"
              type="password"
              required
              class="mb-2"
            ></v-text-field>
            <v-text-field
              v-model="passwordForm.confirmPassword"
              label="Confirm New Password"
              type="password"
              required
              class="mb-4"
            ></v-text-field>
            <v-btn
              color="primary"
              type="submit"
              :loading="isChangingPassword"
              prepend-icon="mdi-lock-reset"
            >
              Change Password
            </v-btn>
          </v-form>
        </v-card-text>
      </v-card>
    </v-card-text>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn
        color="primary"
        size="large"
        @click="savePreferences"
        prepend-icon="mdi-content-save"
      >
        Save Preferences
      </v-btn>
    </v-card-actions>

    <!-- Snackbar -->
    <v-snackbar
      v-model="snackbar.show"
      :color="snackbar.color"
      :timeout="3000"
    >
      {{ snackbar.text }}
    </v-snackbar>
  </v-card>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useRecipeStore } from '../stores/recipes'
import { useAuthStore } from '../stores/auth'

const recipeStore = useRecipeStore()
const authStore = useAuthStore()

const snackbar = ref({
  show: false,
  text: '',
  color: 'success'
})

const preferences = reactive({
  dietRestrictions: [] as string[]
})

const passwordForm = reactive({
  currentPassword: '',
  newPassword: '',
  confirmPassword: ''
})

const isChangingPassword = ref(false)

onMounted(() => {
  // Load existing preferences
  preferences.dietRestrictions = [...recipeStore.userPreferences.dietRestrictions]
})

const savePreferences = () => {
  recipeStore.setUserPreferences(preferences)
  snackbar.value = {
    show: true,
    text: 'Preferences saved successfully!',
    color: 'success'
  }
}

const changePassword = async () => {
  if (passwordForm.newPassword !== passwordForm.confirmPassword) {
    snackbar.value = {
      show: true,
      text: 'New passwords do not match',
      color: 'error'
    }
    return
  }

  try {
    isChangingPassword.value = true
    await authStore.changePassword(passwordForm.currentPassword, passwordForm.newPassword)
    
    snackbar.value = {
      show: true,
      text: 'Password changed successfully',
      color: 'success'
    }
    
    // Clear the form
    passwordForm.currentPassword = ''
    passwordForm.newPassword = ''
    passwordForm.confirmPassword = ''
  } catch (error) {
    snackbar.value = {
      show: true,
      text: 'Failed to change password',
      color: 'error'
    }
  } finally {
    isChangingPassword.value = false
  }
}
</script>