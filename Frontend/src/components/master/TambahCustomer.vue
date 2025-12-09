<template>
  <div class="col-span-2 ">

    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Nama
    </label>
    <div class="w-full mb-4" >
    <input
        type="text"
        value=""
        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
    />
    </div>

    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Alamat
    </label>
    <div class="w-full mb-4" >
    <input
        type="text"
        value=""
        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
    />
    </div>

    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Billing Akun
    </label>
    <div class="w-full mb-4" >
    <input
        type="number"
        value=""
        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
    />
    </div>

 
    <div class="flex items-center gap-3 px-2 mt-6 lg:justify-start w-full">
        <router-link to="/master-data/customer" class="edit-button">
            Batal
        </router-link>
        <button
        type="button"
        class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto"
        >
        Simpan
        </button>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

</script>
