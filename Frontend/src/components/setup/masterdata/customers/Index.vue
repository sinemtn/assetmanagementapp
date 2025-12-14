<template>

    <div>

        <div class="flex justify-between">

            <form>
                <div class="flex gap-2 items-center">

                    <div class="relative">
                        <button class="absolute -translate-y-1/2 left-4 top-1/2">
                            <svg class="fill-gray-500 dark:fill-gray-400" width="20" height="20" viewBox="0 0 20 20"
                                fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" clip-rule="evenodd"
                                    d="M3.04175 9.37363C3.04175 5.87693 5.87711 3.04199 9.37508 3.04199C12.8731 3.04199 15.7084 5.87693 15.7084 9.37363C15.7084 12.8703 12.8731 15.7053 9.37508 15.7053C5.87711 15.7053 3.04175 12.8703 3.04175 9.37363ZM9.37508 1.54199C5.04902 1.54199 1.54175 5.04817 1.54175 9.37363C1.54175 13.6991 5.04902 17.2053 9.37508 17.2053C11.2674 17.2053 13.003 16.5344 14.357 15.4176L17.177 18.238C17.4699 18.5309 17.9448 18.5309 18.2377 18.238C18.5306 17.9451 18.5306 17.4703 18.2377 17.1774L15.418 14.3573C16.5365 13.0033 17.2084 11.2669 17.2084 9.37363C17.2084 5.04817 13.7011 1.54199 9.37508 1.54199Z"
                                    fill="" />
                            </svg>
                        </button>
                        <input type="text" placeholder="Search ..."
                            class="dark:bg-dark-900 h-11 w-full rounded-lg border border-gray-200 bg-transparent py-2.5 pl-12 pr-14 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-800 dark:bg-gray-900 dark:bg-white/[0.03] dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800 " />

                        <button
                            class="absolute right-2.5 top-1/2 inline-flex -translate-y-1/2 items-center gap-0.5 rounded-lg border border-gray-200 bg-gray-50 px-[7px] py-[4.5px] text-xs -tracking-[0.2px] text-gray-500 dark:border-gray-800 dark:bg-white/[0.03] dark:text-gray-400">
                            <span> Search </span>

                        </button>
                    </div>

                    <div class="flex items-center cursor-pointer hover:text-brand-600 transition">
                        <FlagIcon class="mr-1" />
                        <button type="button" class="text-sm text-gray-700" @click="isAdvancedFilter = true">Filter
                            Pencarian</button>
                    </div>

                    <Modal v-if="isAdvancedFilter" @close="isAdvancedFilter = false">
                        <template #body>
                            <div
                                class="no-scrollbar relative w-full max-w-[700px] overflow-y-auto rounded-3xl bg-white p-4 dark:bg-gray-900 lg:p-11">
                                <!-- close btn -->
                                <button @click="isAdvancedFilter = false"
                                    class="transition-color absolute right-5 top-5 z-999 flex h-11 w-11 items-center justify-center rounded-full bg-gray-100 text-gray-400 hover:bg-gray-200 hover:text-gray-600 dark:bg-gray-700 dark:bg-white/[0.05] dark:text-gray-400 dark:hover:bg-white/[0.07] dark:hover:text-gray-300">
                                    <svg class="fill-current" width="24" height="24" viewBox="0 0 24 24" fill="none"
                                        xmlns="http://www.w3.org/2000/svg">
                                        <path fill-rule="evenodd" clip-rule="evenodd"
                                            d="M6.04289 16.5418C5.65237 16.9323 5.65237 17.5655 6.04289 17.956C6.43342 18.3465 7.06658 18.3465 7.45711 17.956L11.9987 13.4144L16.5408 17.9565C16.9313 18.347 17.5645 18.347 17.955 17.9565C18.3455 17.566 18.3455 16.9328 17.955 16.5423L13.4129 12.0002L17.955 7.45808C18.3455 7.06756 18.3455 6.43439 17.955 6.04387C17.5645 5.65335 16.9313 5.65335 16.5408 6.04387L11.9987 10.586L7.45711 6.04439C7.06658 5.65386 6.43342 5.65386 6.04289 6.04439C5.65237 6.43491 5.65237 7.06808 6.04289 7.4586L10.5845 12.0002L6.04289 16.5418Z"
                                            fill="" />
                                    </svg>
                                </button>
                                <div class="px-2 pr-14">
                                    <h4 class="mb-2 text-2xl font-semibold text-gray-800 dark:text-white/90">
                                        Filter Pencarian Printer
                                    </h4>
                                </div>
                                <form class="flex flex-col">
                                    <div class="custom-scrollbar h-[458px] overflow-y-auto p-2">
                                        <div>
                                            <div class="grid grid-cols-1 gap-x-6 gap-y-3 lg:grid-cols-2">


                                                <!-- Kategori Advanced Filter -->
                                                <div class="col-span-2">
                                                    <label
                                                        class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                                        Kategori
                                                    </label>
                                                    <input type="text" value=""
                                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                                                </div>

                                                <!-- MP. No Advanced Filter -->
                                                <div class="col-span-2">
                                                    <label
                                                        class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                                        MP. No
                                                    </label>
                                                    <input type="text" value=""
                                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                                                </div>

                                                <!-- Status Advanced Filter -->
                                                <div class="col-span-2">
                                                    <label
                                                        class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                                        Status
                                                    </label>
                                                    <select
                                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800">
                                                        <option value="">Kontrak</option>
                                                        <option value="">Ready</option>
                                                        <option value="">Kontrak BUP</option>
                                                    </select>
                                                </div>

                                                <!-- Lokasi Advanced Filter -->
                                                <div class="col-span-2">
                                                    <label
                                                        class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                                        Lokasi
                                                    </label>
                                                    <input type="text" value=""
                                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                                                </div>
                                            </div>

                                            <div class="flex items-center gap-3 px-2 mt-6 lg:justify-end">
                                                <button @click="isAdvancedFilter = false" type="button"
                                                    class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto">
                                                    Clear
                                                </button>
                                                <button @click="isAdvancedFilter" type="button"
                                                    class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto">
                                                    Apply
                                                </button>
                                            </div>
                                        </div>
                                    </div>

                                </form>
                            </div>
                        </template>
                    </Modal>


                </div>
            </form>


            <div
                class="rounded-xl border border-gray-200 bg-white p-3 dark:border-gray-800 dark:bg-white/[0.03] md:p-4 hover:shadow-sm transition">
                <router-link to="/stock/printer/tambah" class="block">
                    <span class="flex items-center gap-2">
                        <PlusIcon />

                        Tambah
                    </span>
                </router-link>
            </div>
        </div>

    </div>

    <Modal v-if="isPlusToner" @close="isPlusToner = false">
        <template #body>
            <div
                class="no-scrollbar relative w-full max-w-[700px] overflow-y-auto rounded-3xl bg-white p-4 dark:bg-gray-900 lg:p-11">
                <!-- close btn -->
                <button @click="isPlusToner = false"
                    class="transition-color absolute right-5 top-5 z-999 flex h-11 w-11 items-center justify-center rounded-full bg-gray-100 text-gray-400 hover:bg-gray-200 hover:text-gray-600 dark:bg-gray-700 dark:bg-white/[0.05] dark:text-gray-400 dark:hover:bg-white/[0.07] dark:hover:text-gray-300">
                    <svg class="fill-current" width="24" height="24" viewBox="0 0 24 24" fill="none"
                        xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M6.04289 16.5418C5.65237 16.9323 5.65237 17.5655 6.04289 17.956C6.43342 18.3465 7.06658 18.3465 7.45711 17.956L11.9987 13.4144L16.5408 17.9565C16.9313 18.347 17.5645 18.347 17.955 17.9565C18.3455 17.566 18.3455 16.9328 17.955 16.5423L13.4129 12.0002L17.955 7.45808C18.3455 7.06756 18.3455 6.43439 17.955 6.04387C17.5645 5.65335 16.9313 5.65335 16.5408 6.04387L11.9987 10.586L7.45711 6.04439C7.06658 5.65386 6.43342 5.65386 6.04289 6.04439C5.65237 6.43491 5.65237 7.06808 6.04289 7.4586L10.5845 12.0002L6.04289 16.5418Z"
                            fill="" />
                    </svg>
                </button>
                <div class="px-2 pr-14">
                    <h4 class="mb-2 text-2xl font-semibold text-gray-800 dark:text-white/90">
                        Form Tambah Toner
                    </h4>
                </div>
                <form class="flex flex-col">
                    <div class="custom-scrollbar h-[458px] overflow-y-auto p-2">
                        <div>

                            <div class="grid grid-cols-1 gap-x-6 gap-y-5 lg:grid-cols-2">
                                <div class="col-span-2">
                                    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                        Tipe
                                    </label>
                                    <input type="text" value=""
                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />


                                </div>

                                <div class="col-span-2">
                                    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                        Tambah Qty
                                    </label>
                                    <input type="text" value=""
                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                                </div>

                                <div class="col-span-2">
                                    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                        Customer
                                    </label>

                                    <div class="relative w-full mb-4">
                                        <!-- Input field -->
                                        <input v-model="tipeprinter" type="text" placeholder="Customer"
                                            class="h-11 w-full rounded-lg border border-gray-300 bg-transparent px-4 pr-10 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30"
                                            @keydown.enter="searchTipePrinter" />

                                        <!-- Search button -->
                                        <button type="button" @click="toggleSearchTipePrinter"
                                            class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-brand-600">
                                            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none"
                                                viewBox="0 0 24 24" stroke="currentColor">
                                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                    d="M21 21l-4.35-4.35M11 18a7 7 0 100-14 7 7 0 000 14z" />
                                            </svg>
                                        </button>

                                        <div v-if="showTipePrinter"
                                            class="absolute left-0 z-50 mt-1 w-full max-h-48 overflow-auto rounded-md border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800">
                                            <ul>
                                                <li v-for="(item, index) in filteredTipePrinters" :key="index"
                                                    @click="selectTipePrinter(item)"
                                                    class="cursor-pointer px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700">
                                                    {{ item }}
                                                </li>

                                                <li v-if="filteredTipePrinters.length === 0"
                                                    class="px-3 py-2 text-sm text-gray-500 dark:text-gray-400">
                                                    Tipe Printer Tidak Ditemukan
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>







                                <div class="col-span-2 ">
                                    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                        Keterangan
                                    </label>
                                    <textarea type="text" value=""
                                        class="dark:bg-dark-900 h-20 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="flex items-center gap-3 px-2 mt-6 lg:justify-end">
                        <button @click="isPlusToner = false" type="button"
                            class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto">
                            Batal
                        </button>
                        <button @click="savePlusToner" type="button"
                            class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto">
                            Simpan
                        </button>
                    </div>
                </form>
            </div>
        </template>
    </Modal>

    <Modal v-if="isMinusToner" @close="isMinusToner = false">
        <template #body>
            <div
                class="no-scrollbar relative w-full max-w-[700px] overflow-y-auto rounded-3xl bg-white p-4 dark:bg-gray-900 lg:p-11">
                <!-- close btn -->
                <button @click="isMinusToner = false"
                    class="transition-color absolute right-5 top-5 z-999 flex h-11 w-11 items-center justify-center rounded-full bg-gray-100 text-gray-400 hover:bg-gray-200 hover:text-gray-600 dark:bg-gray-700 dark:bg-white/[0.05] dark:text-gray-400 dark:hover:bg-white/[0.07] dark:hover:text-gray-300">
                    <svg class="fill-current" width="24" height="24" viewBox="0 0 24 24" fill="none"
                        xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M6.04289 16.5418C5.65237 16.9323 5.65237 17.5655 6.04289 17.956C6.43342 18.3465 7.06658 18.3465 7.45711 17.956L11.9987 13.4144L16.5408 17.9565C16.9313 18.347 17.5645 18.347 17.955 17.9565C18.3455 17.566 18.3455 16.9328 17.955 16.5423L13.4129 12.0002L17.955 7.45808C18.3455 7.06756 18.3455 6.43439 17.955 6.04387C17.5645 5.65335 16.9313 5.65335 16.5408 6.04387L11.9987 10.586L7.45711 6.04439C7.06658 5.65386 6.43342 5.65386 6.04289 6.04439C5.65237 6.43491 5.65237 7.06808 6.04289 7.4586L10.5845 12.0002L6.04289 16.5418Z"
                            fill="" />
                    </svg>
                </button>
                <div class="px-2 pr-14">
                    <h4 class="mb-2 text-2xl font-semibold text-gray-800 dark:text-white/90">
                        Form Kurangi Toner
                    </h4>
                </div>
                <form class="flex flex-col ">
                    <div class="custom-scrollbar h-[458px] overflow-y-auto p-2">
                        <div>

                            <div class="">
                                <div class="col-span-2 mb-6">
                                    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                        Tipe
                                    </label>
                                    <select
                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800">
                                        <option value="team-manager">Contract</option>
                                        <option value="developer">Free</option>
                                    </select>
                                </div>

                                <div class="col-span-2 mb-6">
                                    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                        Kurangi Qty
                                    </label>
                                    <input type="text" value=""
                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                                </div>

                                <div class="col-span-2 mb-6">
                                    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                        Jenis Keluar
                                    </label>
                                    <select
                                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800">
                                        <option value="team-manager">KSO</option>
                                        <option value="developer">Jenis Keluar 1</option>
                                    </select>
                                </div>


                                <!-- Kategori -->

                                <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                    Customer
                                </label>

                                <div class="relative w-full mb-4">
                                    <!-- Input field -->
                                    <input v-model="tipeprinter" type="text" placeholder="Customer"
                                        class="h-11 w-full rounded-lg border border-gray-300 bg-transparent px-4 pr-10 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30"
                                        @keydown.enter="searchTipePrinter" />

                                    <!-- Search button -->
                                    <button type="button" @click="toggleSearchTipePrinter"
                                        class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-brand-600">
                                        <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none"
                                            viewBox="0 0 24 24" stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                d="M21 21l-4.35-4.35M11 18a7 7 0 100-14 7 7 0 000 14z" />
                                        </svg>
                                    </button>

                                    <div v-if="showTipePrinter"
                                        class="absolute left-0 z-50 mt-1 w-full max-h-48 overflow-auto rounded-md border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800">
                                        <ul>
                                            <li v-for="(item, index) in filteredTipePrinters" :key="index"
                                                @click="selectTipePrinter(item)"
                                                class="cursor-pointer px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700">
                                                {{ item }}
                                            </li>

                                            <li v-if="filteredTipePrinters.length === 0"
                                                class="px-3 py-2 text-sm text-gray-500 dark:text-gray-400">
                                                Tipe Printer Tidak Ditemukan
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <!-- End Kategori -->

                                <div class="col-span-2 mt-8">
                                    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                                        Keterangan
                                    </label>
                                    <textarea type="text" value=""
                                        class="dark:bg-dark-900 h-20 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                                </div>

                            </div>

                        </div>
                    </div>
                    <div class="flex items-center gap-3 px-2 mt-6 lg:justify-end">
                        <button @click="isMinusToner = false" type="button"
                            class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto">
                            Batal
                        </button>
                        <button @click="saveMinusToner" type="button"
                            class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto">
                            Simpan
                        </button>
                    </div>
                </form>
            </div>
        </template>
    </Modal>




    <div class="overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-gray-800 dark:bg-white/[0.03]">
        <div class="max-w-full overflow-x-auto custom-scrollbar">
            <div>
            </div>
            <table class="min-w-full">
                <thead>
                    <tr class="border-b border-gray-200 dark:border-gray-700">
                        <th class="px-5 py-3 text-left w-1/11 sm:px-6">
                            <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">ID</p>
                        </th>
                        <th class="px-5 py-3 text-left w-2/11 sm:px-6">
                            <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Customer</p>
                        </th>
                        <th class="px-5 py-3 text-left w-2/11 sm:px-6">
                            <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Alamat</p>
                        </th>
                        <th class="px-5 py-3 text-left w-1/11 sm:px-6">
                            <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Aksi</p>
                        </th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-200 dark:divide-gray-700">
                    <tr class="border-t border-gray-100 dark:border-gray-800">
                        <td class="px-5 py-4 sm:px-6">
                            <p class="text-gray-500 text-theme-sm dark:text-gray-400">0001</p>
                        </td>
                        <td class="px-5 py-4 sm:px-6">
                            <p class="text-gray-500 text-theme-sm dark:text-gray-400">PT Maju Sejahtera</p>
                        </td>
                        <td class="px-5 py-4 sm:px-6">
                            <p class="text-gray-500 text-theme-sm dark:text-gray-400">Jl. Ikan Buntek no 2 Waru</p>
                        </td>

                        <td class="px-5 py-4 sm:px-6">
                            <div class="flex items-center gap-2">
                                <router-link to="/surattugas-detail" class="edit-button">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                        stroke-width="1.5" stroke="currentColor" class="size-6">
                                        <path stroke-linecap="round" stroke-linejoin="round"
                                            d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L10.582 16.07a4.5 4.5 0 0 1-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 0 1 1.13-1.897l8.932-8.931Zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0 1 15.75 21H5.25A2.25 2.25 0 0 1 3 18.75V8.25A2.25 2.25 0 0 1 5.25 6H10" />
                                    </svg> Edit
                                </router-link>

                                <router-link to="/surattugas-detail" class="edit-button">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                        stroke-width="1.5" stroke="currentColor" class="size-6">
                                        <path stroke-linecap="round" stroke-linejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21
                                        c.342.052.682.107 1.022.166M4.772 5.79
                                        c.34-.059.68-.114 1.022-.165m0 0
                                        a48.108 48.108 0 0 1 3.478-.397m7.5 0
                                        a48.11 48.11 0 0 1 3.478.397m-10.956 0
                                        V4.5c0-1.18.91-2.164 2.09-2.201
                                        a51.964 51.964 0 0 1 3.32 0
                                        c1.18.037 2.09 1.022 2.09 2.201v1.125M4.772 5.79
                                        L5.82 19.673c.078 1.04.943 1.827 1.989 1.827h8.383
                                        c1.046 0 1.91-.787 1.989-1.827L19.228 5.79" />
                                    </svg>
                                    Delete
                                </router-link>

                            </div>
                        </td>
                    </tr>

                    <tr class="border-t border-gray-100 dark:border-gray-800">
                        <td class="px-5 py-4 sm:px-6">
                            <p class="text-gray-500 text-theme-sm dark:text-gray-400">0002</p>
                        </td>
                        <td class="px-5 py-4 sm:px-6">
                            <p class="text-gray-500 text-theme-sm dark:text-gray-400">PT Info Global</p>
                        </td>
                        <td class="px-5 py-4 sm:px-6">
                            <p class="text-gray-500 text-theme-sm dark:text-gray-400">Jl. Hamzah Haz no 3A</p>
                        </td>

                        <td class="px-5 py-4 sm:px-6">
                            <div class="flex items-center gap-2">
                                <router-link to="/surattugas-detail" class="edit-button">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                        stroke-width="1.5" stroke="currentColor" class="size-6">
                                        <path stroke-linecap="round" stroke-linejoin="round"
                                            d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L10.582 16.07a4.5 4.5 0 0 1-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 0 1 1.13-1.897l8.932-8.931Zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0 1 15.75 21H5.25A2.25 2.25 0 0 1 3 18.75V8.25A2.25 2.25 0 0 1 5.25 6H10" />
                                    </svg> Edit
                                </router-link>

                                <router-link to="/surattugas-detail" class="edit-button">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                        stroke-width="1.5" stroke="currentColor" class="size-6">
                                        <path stroke-linecap="round" stroke-linejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21
                                        c.342.052.682.107 1.022.166M4.772 5.79
                                        c.34-.059.68-.114 1.022-.165m0 0
                                        a48.108 48.108 0 0 1 3.478-.397m7.5 0
                                        a48.11 48.11 0 0 1 3.478.397m-10.956 0
                                        V4.5c0-1.18.91-2.164 2.09-2.201
                                        a51.964 51.964 0 0 1 3.32 0
                                        c1.18.037 2.09 1.022 2.09 2.201v1.125M4.772 5.79
                                        L5.82 19.673c.078 1.04.943 1.827 1.989 1.827h8.383
                                        c1.046 0 1.91-.787 1.989-1.827L19.228 5.79" />
                                    </svg>
                                    Delete
                                </router-link>

                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';


import FlagIcon from '@/icons/FlagIcon.vue';
import Modal from '@/components/profile/Modal.vue';
import EditIconVue from '@/icons/EditIcon.vue';

const isPlusToner = ref(false)

const savePlusToner = () => {
    console.log('Toner Ditambahkan')
    isPlusToner.value = false
}

const isMinusToner = ref(false)

const saveMinusToner = () => {
    console.log('Profile saved')
    isMinusToner.value = false
}


const stocktoners = ref([]);
const loading = ref(true);

onMounted(async () => {
    try {
        const response = await axios.get('https://103bec10-ae2f-4484-bfc7-16921f4b1b5e.mock.pstmn.io/api/stock/toner')
        stocktoners.value = response.data.data
    } catch (error) {
        console.error('Gagal Fetching Data:', error)
    } finally {
        loading.value = false
    }

})


// Modal Advanced Filter
const isAdvancedFilter = ref(false);
const AdvancedFilter = () => {
    console.log('Filter Opened')
    isAdvancedFilter.value = true
}
</script>

<style scoped>
/* Add any additional styles here if needed */
</style>
