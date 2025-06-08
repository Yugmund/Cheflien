<template>
  <v-card class="mb-6">
    <v-card-text>
      <v-row>
        <v-col cols="12" sm="6" md="4">
          <v-combobox
            v-model="localSelectedIngredients"
            :items="availableIngredients"
            label="Filter by ingredients"
            multiple
            chips
            closable-chips
            prepend-icon="mdi-food-variant"
          ></v-combobox>
        </v-col>
        <v-col cols="12" sm="6" md="4">
          <v-select
            v-model="localCookingTime"
            :items="cookingTimeOptions"
            label="Cooking time"
            prepend-icon="mdi-clock-outline"
          ></v-select>
        </v-col>
        <v-col cols="12" sm="6" md="4">
          <v-btn
            color="primary"
            block
            @click="applyFilters"
            prepend-icon="mdi-filter"
          >
            Apply Filters
          </v-btn>
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'

interface Props {
  selectedIngredients: string[]
  cookingTime: string
}

const props = defineProps<Props>()

const emit = defineEmits<{
  'update:selectedIngredients': [value: string[]]
  'update:cookingTime': [value: string]
  'apply-filters': []
}>()

const localSelectedIngredients = ref([...props.selectedIngredients])
const localCookingTime = ref(props.cookingTime)

const cookingTimeOptions = [
  { title: 'Any', value: '' },
  { title: 'Under 30 mins', value: '30' },
  { title: 'Under 45 mins', value: '45' },
  { title: 'Under 60 mins', value: '60' }
]

const availableIngredients = [
  'Pasta',
  'Chicken',
  'Beef',
  'Fish',
  'Tomatoes',
  'Cheese',
  'Vegetables',
  'Rice',
  'Eggs',
  'Garlic',
  'Mushrooms',
  'Onions',
  'Bell Peppers',
  'Spinach',
  'Broccoli',
  'Carrots',
  'Potatoes',
  'Beans',
  'Quinoa',
  'Avocado',
  'Shrimp',
  'Salmon',
  'Tofu',
  'Coconut Milk',
  'Lemon',
  'Herbs',
  'Olive Oil'
]

watch(localSelectedIngredients, (newValue) => {
  emit('update:selectedIngredients', newValue)
})

watch(localCookingTime, (newValue) => {
  emit('update:cookingTime', newValue)
})

const applyFilters = () => {
  emit('apply-filters')
}
</script>