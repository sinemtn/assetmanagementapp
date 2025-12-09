<template>
  <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-7 md:gap-6 mt-5">
    <router-link to="/master-data/customer" class="block">
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
          Data Customer
        </p>
      </div>
    </router-link>

    <!-- <router-link to="master-data/printer" class="block">
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
            d="M6 9V4h12v5M6 18h12v-5H6v5Zm0 0H4a1 1 0 0 1-1-1v-6a1 1 0 0 1 1-1h16a1 1 0 0 1 1 1v6a1 1 0 0 1-1 1h-2"
          />
        </svg>
      </div>

      <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
        Data Printer
      </p>
    </div>
    </router-link> -->

     <!-- <router-link to="" class="block">
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
            d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 0 0 2.573 1.039c1.543-.893 3.31.874 2.417 2.417a1.724 1.724 0 0 0 1.04 2.573c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 0 0-1.04 2.573c.893 1.543-.874 3.31-2.417 2.417a1.724 1.724 0 0 0-2.573 1.04c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 0 0-2.573-1.04c-1.543.893-3.31-.874-2.417-2.417a1.724 1.724 0 0 0-1.04-2.573c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 0 0 1.04-2.573c-.893-1.543.874-3.31 2.417-2.417.997.576 2.252.217 2.573-1.04z"
          />
          <circle cx="12" cy="12" r="3" />
        </svg>
      </div>

      <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
        Data Sparepart
      </p>
    </div>
    </router-link> -->

  </div>
</template>
