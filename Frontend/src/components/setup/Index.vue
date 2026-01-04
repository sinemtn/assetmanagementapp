<template>
  <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-7 md:gap-6 mt-5">
    <router-link to="/settings/masterdata" class="block">
      <div class="flex flex-col items-center justify-center">
        <div
          class="flex items-center justify-center w-20 h-20 rounded-full bg-gradient-to-b from-blue-500 to-blue-600 shadow-md"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            class="w-8 h-8 text-white"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            stroke-width="2.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          >
            <path d="M3 6.5A2.5 2.5 0 0 1 5.5 4h4l2 2h7A2.5 2.5 0 0 1 21 8.5v9A2.5 2.5 0 0 1 18.5 20h-13A2.5 2.5 0 0 1 3 17.5v-11z" />
          </svg>
        </div>

        <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
          Master Data
        </p>
      </div>
    </router-link>
  </div>
</template>
