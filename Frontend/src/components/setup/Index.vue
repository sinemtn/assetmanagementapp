<template>
  <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-7 md:gap-6 mt-5">
    <router-link to="/setup/printer" class="block">
      <div class="flex flex-col items-center justify-center">
        <div
          class="flex items-center justify-center w-20 h-20 rounded-full bg-gradient-to-b from-blue-500 to-blue-600 shadow-md"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            class="w-8 h-8 text-white"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            stroke-width="2"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M15 19a6 6 0 0 0-6 0M12 12a4 4 0 1 0 0-8 4 4 0 0 0 0 8Zm9 6a9 9 0 1 0-18 0"
            />
          </svg>
        </div>

        <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
          Master Data
        </p>
      </div>
    </router-link>

    <router-link to="/setup/printer" class="block">
      <div class="flex flex-col items-center justify-center">
        <div
          class="flex items-center justify-center w-20 h-20 rounded-full bg-gradient-to-b from-blue-500 to-blue-600 shadow-md"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            class="w-8 h-8 text-white"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            stroke-width="2"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M15 19a6 6 0 0 0-6 0M12 12a4 4 0 1 0 0-8 4 4 0 0 0 0 8Zm9 6a9 9 0 1 0-18 0"
            />
          </svg>
        </div>

        <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
          User
        </p>
      </div>
    </router-link>


  </div>
</template>
