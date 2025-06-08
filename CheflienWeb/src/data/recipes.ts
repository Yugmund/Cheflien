export interface Recipe {
  id: number
  title: string
  description: string
  image: string
  cookingTime: number
  servings: number
  difficulty: string
  ingredients: string[]
  dietaryTags: string[]
  instructions: string[]
}

export const recipes: Recipe[] = [
  {
    id: 1,
    title: 'Spaghetti Carbonara',
    description: 'Classic Italian pasta dish with eggs, cheese, pancetta, and black pepper.',
    image: 'https://images.pexels.com/photos/1527603/pexels-photo-1527603.jpeg',
    cookingTime: 30,
    servings: 4,
    difficulty: 'Medium',
    ingredients: [
      '400g spaghetti',
      '200g pancetta or guanciale, diced',
      '4 large eggs',
      '100g Pecorino Romano, grated',
      '100g Parmigiano Reggiano, grated',
      '2 cloves garlic, minced',
      'Black pepper, freshly ground',
      'Salt to taste'
    ],
    dietaryTags: [],
    instructions: [
      'Bring a large pot of salted water to boil. Add spaghetti and cook according to package instructions.',
      'While pasta cooks, heat a large pan over medium heat. Add pancetta and cook until crispy, about 5-7 minutes.',
      'In a bowl, whisk together eggs, grated cheeses, and plenty of black pepper.',
      'Reserve 1 cup of pasta water, then drain pasta.',
      'Working quickly, add hot pasta to the pan with pancetta, remove from heat.',
      'Pour egg mixture over pasta, stirring quickly to coat the pasta and create a creamy sauce.',
      'Add pasta water as needed to achieve desired consistency.',
      'Serve immediately with extra grated cheese and black pepper.'
    ]
  },
  {
    id: 2,
    title: 'Vegetable Stir-Fry',
    description: 'Quick and healthy Asian-inspired vegetable stir-fry with tofu.',
    image: 'https://images.pexels.com/photos/1640774/pexels-photo-1640774.jpeg',
    cookingTime: 25,
    servings: 3,
    difficulty: 'Easy',
    ingredients: [
      '200g firm tofu, cubed',
      '2 cups mixed vegetables (bell peppers, broccoli, carrots)',
      '2 cloves garlic, minced',
      '1 tbsp ginger, minced',
      '2 tbsp soy sauce',
      '1 tbsp sesame oil',
      '1 cup cooked rice'
    ],
    dietaryTags: ['vegetarian', 'vegan', 'glutenFree', 'dairyFree', 'soyFree'],
    instructions: [
      'Heat oil in a large wok or skillet over high heat.',
      'Add tofu and cook until golden brown on all sides.',
      'Add garlic and ginger, stir-fry for 30 seconds.',
      'Add vegetables and stir-fry for 3-4 minutes until crisp-tender.',
      'Add soy sauce and sesame oil, toss to combine.',
      'Serve over rice.'
    ]
  },
  {
    id: 3,
    title: 'Grilled Salmon',
    description: 'Fresh salmon fillet with lemon herb butter sauce.',
    image: 'https://images.pexels.com/photos/3763847/pexels-photo-3763847.jpeg',
    cookingTime: 20,
    servings: 2,
    difficulty: 'Easy',
    ingredients: [
      '2 salmon fillets',
      '2 tbsp butter',
      '1 lemon, juiced and zested',
      '2 cloves garlic, minced',
      'Fresh herbs (dill, parsley)',
      'Salt and pepper'
    ],
    dietaryTags: ['glutenFree', 'keto', 'lowCarb', 'lactoseFree'],
    instructions: [
      'Preheat grill to medium-high heat.',
      'Season salmon with salt and pepper.',
      'Grill salmon for 4-5 minutes per side.',
      'Meanwhile, melt butter with garlic, lemon juice, and herbs.',
      'Serve salmon with herb butter sauce.'
    ]
  },
  {
    id: 4,
    title: 'Chicken Alfredo',
    description: 'Creamy pasta dish with grilled chicken and parmesan sauce.',
    image: 'https://images.pexels.com/photos/1437267/pexels-photo-1437267.jpeg',
    cookingTime: 45,
    servings: 4,
    difficulty: 'Medium',
    ingredients: ['Pasta', 'Chicken', 'Cheese', 'Garlic'],
    dietaryTags: [],
    instructions: [
      'Cook pasta according to package instructions.',
      'Season and grill chicken breast until cooked through.',
      'Make alfredo sauce with butter, cream, and parmesan.',
      'Combine pasta, chicken, and sauce.',
      'Serve hot with extra parmesan.'
    ]
  },
  {
    id: 5,
    title: 'Beef Stir-Fry',
    description: 'Quick and flavorful beef stir-fry with mixed vegetables.',
    image: 'https://images.pexels.com/photos/2313686/pexels-photo-2313686.jpeg',
    cookingTime: 35,
    servings: 4,
    difficulty: 'Medium',
    ingredients: ['Beef', 'Vegetables', 'Rice', 'Garlic'],
    dietaryTags: ['glutenFree', 'dairyFree'],
    instructions: [
      'Slice beef thinly against the grain.',
      'Heat oil in wok over high heat.',
      'Stir-fry beef until browned.',
      'Add vegetables and cook until tender-crisp.',
      'Season with soy sauce and serve over rice.'
    ]
  },
  {
    id: 6,
    title: 'Quinoa Buddha Bowl',
    description: 'Nutritious bowl with quinoa, roasted vegetables, and tahini dressing.',
    image: 'https://images.pexels.com/photos/1640777/pexels-photo-1640777.jpeg',
    cookingTime: 40,
    servings: 2,
    difficulty: 'Easy',
    ingredients: ['Quinoa', 'Vegetables', 'Avocado'],
    dietaryTags: ['vegetarian', 'vegan', 'glutenFree', 'dairyFree'],
    instructions: [
      'Cook quinoa according to package instructions.',
      'Roast vegetables in oven until tender.',
      'Make tahini dressing with tahini, lemon, and garlic.',
      'Assemble bowl with quinoa, vegetables, and avocado.',
      'Drizzle with dressing and serve.'
    ]
  },
  {
    id: 7,
    title: 'Mushroom Risotto',
    description: 'Creamy Italian rice dish with wild mushrooms and parmesan.',
    image: 'https://images.pexels.com/photos/1438672/pexels-photo-1438672.jpeg',
    cookingTime: 50,
    servings: 4,
    difficulty: 'Hard',
    ingredients: ['Rice', 'Mushrooms', 'Cheese', 'Onions'],
    dietaryTags: ['vegetarian', 'glutenFree'],
    instructions: [
      'Sauté mushrooms until golden.',
      'Heat stock in separate pan.',
      'Cook onions until translucent.',
      'Add rice and toast for 2 minutes.',
      'Add stock gradually, stirring constantly.',
      'Finish with cheese and mushrooms.'
    ]
  },
  {
    id: 8,
    title: 'Greek Salad',
    description: 'Fresh Mediterranean salad with tomatoes, olives, and feta cheese.',
    image: 'https://images.pexels.com/photos/1213710/pexels-photo-1213710.jpeg',
    cookingTime: 15,
    servings: 3,
    difficulty: 'Easy',
    ingredients: ['Tomatoes', 'Cheese', 'Vegetables'],
    dietaryTags: ['vegetarian', 'glutenFree', 'keto', 'lowCarb'],
    instructions: [
      'Chop tomatoes, cucumbers, and onions.',
      'Add olives and feta cheese.',
      'Dress with olive oil and lemon.',
      'Season with oregano and salt.',
      'Serve immediately.'
    ]
  },
  {
    id: 9,
    title: 'Lentil Curry',
    description: 'Spicy and aromatic Indian curry with red lentils and coconut milk.',
    image: 'https://images.pexels.com/photos/2474661/pexels-photo-2474661.jpeg',
    cookingTime: 35,
    servings: 4,
    difficulty: 'Medium',
    ingredients: ['Beans', 'Vegetables', 'Rice', 'Garlic', 'Coconut Milk'],
    dietaryTags: ['vegetarian', 'vegan', 'glutenFree', 'dairyFree'],
    instructions: [
      'Rinse and sort lentils.',
      'Sauté onions, garlic, and spices.',
      'Add lentils and coconut milk.',
      'Simmer until lentils are tender.',
      'Serve over rice with fresh cilantro.'
    ]
  },
  {
    id: 10,
    title: 'Chicken Caesar Salad',
    description: 'Classic salad with grilled chicken, romaine lettuce, and caesar dressing.',
    image: 'https://images.pexels.com/photos/2097090/pexels-photo-2097090.jpeg',
    cookingTime: 20,
    servings: 2,
    difficulty: 'Easy',
    ingredients: ['Chicken', 'Vegetables', 'Cheese'],
    dietaryTags: ['glutenFree', 'keto', 'lowCarb'],
    instructions: [
      'Grill chicken breast until cooked through.',
      'Wash and chop romaine lettuce.',
      'Make caesar dressing with anchovies and parmesan.',
      'Toss lettuce with dressing.',
      'Top with sliced chicken and croutons.'
    ]
  },
  {
    id: 11,
    title: 'Vegetable Lasagna',
    description: 'Layered pasta dish with roasted vegetables and ricotta cheese.',
    image: 'https://images.pexels.com/photos/4518843/pexels-photo-4518843.jpeg',
    cookingTime: 75,
    servings: 6,
    difficulty: 'Hard',
    ingredients: ['Pasta', 'Vegetables', 'Cheese', 'Tomatoes'],
    dietaryTags: ['vegetarian'],
    instructions: [
      'Roast vegetables until tender.',
      'Cook lasagna noodles al dente.',
      'Mix ricotta with herbs and eggs.',
      'Layer noodles, vegetables, and cheese.',
      'Bake until golden and bubbly.'
    ]
  },
  {
    id: 12,
    title: 'Shrimp Tacos',
    description: 'Fresh corn tortillas filled with seasoned shrimp and avocado.',
    image: 'https://images.pexels.com/photos/2092507/pexels-photo-2092507.jpeg',
    cookingTime: 25,
    servings: 3,
    difficulty: 'Easy',
    ingredients: ['Shrimp', 'Avocado', 'Vegetables'],
    dietaryTags: ['glutenFree', 'dairyFree'],
    instructions: [
      'Season shrimp with spices.',
      'Cook shrimp until pink and cooked through.',
      'Warm corn tortillas.',
      'Mash avocado with lime and salt.',
      'Assemble tacos with shrimp, avocado, and vegetables.'
    ]
  },
  {
    id: 13,
    title: 'Thai Green Curry',
    description: 'Aromatic Thai curry with coconut milk, vegetables, and fresh herbs.',
    image: 'https://images.pexels.com/photos/2347311/pexels-photo-2347311.jpeg',
    cookingTime: 30,
    servings: 4,
    difficulty: 'Medium',
    ingredients: ['Coconut Milk', 'Vegetables', 'Rice', 'Herbs', 'Chicken'],
    dietaryTags: ['glutenFree', 'dairyFree'],
    instructions: [
      'Heat coconut milk in a large pan.',
      'Add green curry paste and cook until fragrant.',
      'Add chicken and cook until done.',
      'Add vegetables and simmer until tender.',
      'Serve over jasmine rice with fresh basil.'
    ]
  },
  {
    id: 14,
    title: 'Mediterranean Quinoa Salad',
    description: 'Light and refreshing salad with quinoa, olives, and fresh vegetables.',
    image: 'https://images.pexels.com/photos/1640770/pexels-photo-1640770.jpeg',
    cookingTime: 25,
    servings: 4,
    difficulty: 'Easy',
    ingredients: ['Quinoa', 'Tomatoes', 'Vegetables', 'Olive Oil', 'Lemon'],
    dietaryTags: ['vegetarian', 'vegan', 'glutenFree', 'dairyFree'],
    instructions: [
      'Cook quinoa and let cool.',
      'Dice tomatoes, cucumbers, and red onion.',
      'Mix quinoa with vegetables and olives.',
      'Whisk olive oil with lemon juice and herbs.',
      'Toss salad with dressing and serve chilled.'
    ]
  },
  {
    id: 15,
    title: 'Beef Tacos',
    description: 'Seasoned ground beef in soft tortillas with fresh toppings.',
    image: 'https://images.pexels.com/photos/2092507/pexels-photo-2092507.jpeg',
    cookingTime: 20,
    servings: 4,
    difficulty: 'Easy',
    ingredients: ['Beef', 'Tomatoes', 'Cheese', 'Avocado'],
    dietaryTags: ['glutenFree'],
    instructions: [
      'Brown ground beef with taco seasoning.',
      'Warm tortillas in dry pan.',
      'Dice tomatoes, onions, and avocado.',
      'Assemble tacos with beef and toppings.',
      'Serve with lime wedges and hot sauce.'
    ]
  },
  {
    id: 16,
    title: 'Mushroom Stroganoff',
    description: 'Creamy mushroom dish served over egg noodles.',
    image: 'https://images.pexels.com/photos/1438672/pexels-photo-1438672.jpeg',
    cookingTime: 35,
    servings: 4,
    difficulty: 'Medium',
    ingredients: ['Mushrooms', 'Pasta', 'Onions', 'Garlic'],
    dietaryTags: ['vegetarian'],
    instructions: [
      'Sauté mushrooms until golden brown.',
      'Cook onions and garlic until soft.',
      'Add flour and cook for 1 minute.',
      'Gradually add broth and cream.',
      'Serve over cooked egg noodles.'
    ]
  },
  {
    id: 17,
    title: 'Asian Lettuce Wraps',
    description: 'Light and flavorful chicken lettuce wraps with Asian seasonings.',
    image: 'https://images.pexels.com/photos/1640774/pexels-photo-1640774.jpeg',
    cookingTime: 20,
    servings: 3,
    difficulty: 'Easy',
    ingredients: ['Chicken', 'Vegetables', 'Garlic'],
    dietaryTags: ['glutenFree', 'dairyFree', 'lowCarb', 'keto'],
    instructions: [
      'Cook ground chicken with garlic and ginger.',
      'Add soy sauce and sesame oil.',
      'Wash and separate lettuce leaves.',
      'Fill lettuce cups with chicken mixture.',
      'Garnish with green onions and sesame seeds.'
    ]
  },
  {
    id: 18,
    title: 'Caprese Stuffed Chicken',
    description: 'Chicken breast stuffed with mozzarella, tomatoes, and basil.',
    image: 'https://images.pexels.com/photos/1437267/pexels-photo-1437267.jpeg',
    cookingTime: 40,
    servings: 4,
    difficulty: 'Medium',
    ingredients: ['Chicken', 'Cheese', 'Tomatoes', 'Herbs'],
    dietaryTags: ['glutenFree', 'keto', 'lowCarb'],
    instructions: [
      'Butterfly chicken breasts and pound thin.',
      'Layer mozzarella, tomatoes, and basil inside.',
      'Roll up and secure with toothpicks.',
      'Sear in hot pan until golden.',
      'Finish in oven until cooked through.'
    ]
  },
  {
    id: 19,
    title: 'Coconut Rice Bowl',
    description: 'Fragrant coconut rice with roasted vegetables and peanut sauce.',
    image: 'https://images.pexels.com/photos/1640777/pexels-photo-1640777.jpeg',
    cookingTime: 45,
    servings: 3,
    difficulty: 'Medium',
    ingredients: ['Rice', 'Coconut Milk', 'Vegetables', 'Tofu'],
    dietaryTags: ['vegetarian', 'vegan', 'glutenFree', 'dairyFree'],
    instructions: [
      'Cook rice in coconut milk and water.',
      'Roast vegetables until caramelized.',
      'Pan-fry tofu until crispy.',
      'Make peanut sauce with peanut butter and soy sauce.',
      'Assemble bowls and drizzle with sauce.'
    ]
  },
  {
    id: 20,
    title: 'Stuffed Bell Peppers',
    description: 'Bell peppers stuffed with ground turkey, rice, and vegetables.',
    image: 'https://images.pexels.com/photos/1640774/pexels-photo-1640774.jpeg',
    cookingTime: 55,
    servings: 4,
    difficulty: 'Medium',
    ingredients: ['Bell Peppers', 'Rice', 'Chicken', 'Tomatoes', 'Cheese'],
    dietaryTags: ['glutenFree'],
    instructions: [
      'Cut tops off peppers and remove seeds.',
      'Cook rice and mix with ground turkey.',
      'Add diced tomatoes and seasonings.',
      'Stuff peppers with mixture.',
      'Bake until peppers are tender and filling is hot.'
    ]
  }
]