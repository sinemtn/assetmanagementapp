<template>
    <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-7 md:gap-6 mt-5">

        <!-- Customers -->
        <router-link to="/settings/masterdata/customers" class="block">
            <div class="flex flex-col items-center justify-center">
                <div
                    class="flex items-center justify-center w-20 h-20 rounded-full bg-gradient-to-b from-blue-500 to-blue-600 shadow-md">
                    <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8 text-white" viewBox="0 0 24 24"
                        fill="currentColor">
                        <circle cx="6.8" cy="9" r="2.8" />
                        <path d="M2.8 20c0-2.6 2.2-4.4 4-4.4s4 1.8 4 4.4v1h-8v-1z" />

                        <circle cx="17.2" cy="9" r="2.8" />
                        <path d="M13.2 20c0-2.6 2.2-4.4 4-4.4s4 1.8 4 4.4v1h-8v-1z" />

                        <circle cx="12" cy="7" r="3.2" />
                        <path d="M6 20c0-3.2 2.8-5.4 6-5.4s6 2.2 6 5.4v1H6v-1z" />
                    </svg>

                </div>

                <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
                    Customers
                </p>
            </div>
        </router-link>

        <!-- Suppliers -->
        <router-link to="/settings/masterdata" class="block">
            <div class="flex flex-col items-center justify-center">
                <div
                    class="flex items-center justify-center w-20 h-20 rounded-full bg-gradient-to-b from-blue-500 to-blue-600 shadow-md">
                    <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8 text-white" viewBox="0 0 24 24"
                        fill="currentColor">
                        <!-- Handle -->
                        <path d="M3 4h2l2.2 9.5c.2.9 1 1.5 1.9 1.5h8.6c.9 0 1.7-.6 1.9-1.5L21 7H7" />

                        <!-- Basket -->
                        <rect x="9" y="8" width="9" height="5" rx="0.8" />

                        <!-- Wheels -->
                        <circle cx="10" cy="18" r="1.5" />
                        <circle cx="17" cy="18" r="1.5" />
                    </svg>


                </div>
                <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
                    Suppliers
                </p>
            </div>
        </router-link>

        <!-- Location -->
        <router-link to="/settings/masterdata" class="block">
            <div class="flex flex-col items-center justify-center">
                <div
                    class="flex items-center justify-center w-20 h-20 rounded-full bg-gradient-to-b from-blue-500 to-blue-600 shadow-md">
                    <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8 text-white" viewBox="0 0 24 24" fill="none"
                        stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                        <path d="M12 2c-3.3 0-6 2.5-6 5.6 0 4.1 6 11.8 6 11.8s6-7.7 6-11.8C18 4.5 15.3 2 12 2z" />
                        <circle cx="12" cy="8" r="2.2" />
                    </svg>
                </div>
                <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
                    Location
                </p>
            </div>
        </router-link>

        <!-- Users -->
        <router-link to="/settings/masterdata" class="block">
            <div class="flex flex-col items-center justify-center">
                <div
                    class="flex items-center justify-center w-20 h-20 rounded-full bg-gradient-to-b from-blue-500 to-blue-600 shadow-md">
                    <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8 text-white" viewBox="0 0 24 24"
                        fill="currentColor">
                        <!-- Head -->
                        <circle cx="12" cy="7" r="3.5" />
                        <!-- Body -->
                        <path d="M5 20c0-3.5 3-6 7-6s7 2.5 7 6v1H5v-1z" />
                    </svg>



                </div>
                <p class="mt-3 text-sm font-bold text-gray-700 dark:text-gray-300">
                    Users
                </p>
            </div>
        </router-link>

    </div>
</template>
