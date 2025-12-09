<template>

  <div>
    <!-- Tab Buttons -->
    <div class="inline-flex rounded-xl border border-gray-200 bg-gray-50 p-1 dark:border-gray-700 dark:bg-gray-800">
      <button
        v-for="tab in tabs"
        :key="tab"
        @click="activeTab = tab"
        :class="[
          'px-4 py-2 text-sm font-medium rounded-lg transition',
          activeTab === tab
            ? 'bg-white shadow text-gray-900 dark:bg-gray-900 dark:text-white'
            : 'text-gray-500 hover:text-gray-800 dark:text-gray-400 dark:hover:text-gray-200'
        ]"
      >
        {{ tab }}
      </button>
    </div>

    <!-- Tab Content -->
    <div class="mt-6">
      <div v-if="activeTab === 'General'" class="text-gray-800 dark:text-gray-200">


    <!-- <div class="flex mb-4 gap-6">
 
      <div class="w-1/2">
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
          No. MP
      </label>
        <div class="w-full" >
          
            <input
                type="text"
                value="006/LJ/HP-Printer/example"
                class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
            />
        </div>
      </div>
    


      <div class="w-1/2">
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
          Status
      </label>

      <div class="w-full" >
      <select
          class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800"
          >
          <option value=""> Apa </option>
          <option value=""> ApA??? </option>
      </select>
      </div>
      </div>
   
    </div> -->
    <!-- No Komplain -->
    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        No. MP
    </label>
    <div class="w-full mb-4" >
       
        <input
            type="text"
            value=""
            class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
        />
    </div>
    <!-- End of No Komplain -->

    <!-- No Komplain -->
    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Status
    </label>
    <div class="w-full mb-4" >
        <select
        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800"
        >
        <option value=""> Troubleshoot </option>
        <option value=""> Maintenance </option>
    </select>
    </div>
    <!-- End of No Komplain -->



    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Customer
    </label>

    <div class="relative w-full mb-4">
    <!-- Input field -->
    <input
        v-model="tipeprinter"
        type="text"
        placeholder="Cari Customer"
        class="h-11 w-full rounded-lg border border-gray-300 bg-transparent px-4 pr-10 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30"
        @keydown.enter="searchTipePrinter"
    />

    <!-- Search button -->
    <button
        type="button"
        @click="toggleSearchTipePrinter"
        class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-brand-600"
    >
        <svg
        xmlns="http://www.w3.org/2000/svg"
        class="h-5 w-5"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        >
        <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M21 21l-4.35-4.35M11 18a7 7 0 100-14 7 7 0 000 14z"
        />
        </svg>
    </button>

    <div
        v-if="showTipePrinter"
        class="absolute left-0 z-50 mt-1 w-full max-h-48 overflow-auto rounded-md border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800"
    >
        <ul>
        <li
            v-for="(item, index) in filteredTipePrinters"
            :key="index"
            @click="selectTipePrinter(item)"
            class="cursor-pointer px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700"
        >
            {{ item }}
        </li>

        <li
            v-if="filteredTipePrinters.length === 0"
            class="px-3 py-2 text-sm text-gray-500 dark:text-gray-400"
        >
            Tipe Printer Tidak Ditemukan
        </li>
        </ul>
    </div>
    </div>
   
    <!-- End Tipe -->

    <!-- Jenis Tugas -->
    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Jenis Tugas
    </label>
    <div class="w-full mb-4" >
    <select
        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800"
        >
        <option value=""> Troubleshoot </option>
        <option value=""> Maintenance </option>
    </select>
    </div>
    <!-- End of Jenis Tugas -->

    <!-- No Komplain -->
    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        No. Komplain
    </label>
    <div class="w-full mb-4" >
       
        <input
            type="text"
            value=""
            class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
        />
    </div>
    <!-- End of No Komplain -->

     <!-- Surat Tugas -->
    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Surat Tugas
    </label>
    <div class="w-full mb-4" >
       
        <input
            type="text"
            value=""
            class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
        />
    </div>
    <!-- End of Surat Tugas -->

    <!-- Lokasi -->
    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Lokasi
    </label>
    <div class="w-full mb-4" >
    <select
        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800"
        >
        <option value=""> Warehouse </option>
        <option value=""> Pantry </option>
    </select>
    </div>
    <!-- End of Lokasi -->

    <!-- PIC -->

    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        PIC
    </label>

    <div class="relative w-full mb-4">
    <!-- Input field -->
    <input
        v-model="tipeprinter"
        type="text"
        placeholder="Cari PIC ... "
        class="h-11 w-full rounded-lg border border-gray-300 bg-transparent px-4 pr-10 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30"
        @keydown.enter="searchTipePrinter"
    />

    <!-- Search button -->
    <button
        type="button"
        @click="toggleSearchTipePrinter"
        class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-brand-600"
    >
        <svg
        xmlns="http://www.w3.org/2000/svg"
        class="h-5 w-5"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        >
        <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M21 21l-4.35-4.35M11 18a7 7 0 100-14 7 7 0 000 14z"
        />
        </svg>
    </button>

    <div
        v-if="showTipePrinter"
        class="absolute left-0 z-50 mt-1 w-full max-h-48 overflow-auto rounded-md border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800"
    >
        <ul>
        <li
            v-for="(item, index) in filteredTipePrinters"
            :key="index"
            @click="selectTipePrinter(item)"
            class="cursor-pointer px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700"
        >
            {{ item }}
        </li>

        <li
            v-if="filteredTipePrinters.length === 0"
            class="px-3 py-2 text-sm text-gray-500 dark:text-gray-400"
        >
            Tipe Printer Tidak Ditemukan
        </li>
        </ul>
    </div>
    </div>
    <!-- End PIC -->

    <div class="flex items-center gap-3 px-2 mt-6">
      <button
        @click="isProfileInfoModal = false"
        type="button"
        class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto"
      >
        Batal
      </button>
      <button
        @click="saveProfile"
        type="button"
        class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto"
      >
        Simpan
      </button>
      <button
        @click="saveProfile"
        type="button"
        class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto"
      >
        Authorize
      </button>
    </div>

    </div>

      <div v-else-if="activeTab === 'Items'" class="text-gray-800 dark:text-gray-200">


       <table class="min-w-full">
        <thead>
          <tr class="border-b border-gray-200 dark:border-gray-700">
            <th class="px-5 py-3 text-left w-3/11 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Tipe</p>
            </th>
            <th class="px-5 py-3 text-left w-2/11 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Kategori</p>
            </th>
            <th class="px-5 py-3 text-left w-2/11 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Total Stok</p>
            </th>
          </tr>
        </thead>
        <tbody class="divide-y divide-gray-200 dark:divide-gray-700">
          <tr
            v-for="(stditem, index) in stditems"
            :key="index"
            class="border-t border-gray-100 dark:border-gray-800"
          >
            <td class="px-5 py-4 sm:px-6">
              <p class="text-gray-500 text-theme-sm dark:text-gray-400">{{ stditem.nama }}</p>
            </td>
            <td class="px-5 py-4 sm:px-6">
              <p class="text-gray-500 text-theme-sm dark:text-gray-400">{{ stditem.kategori }}</p>
            </td>
            <td class="px-5 py-4 sm:px-6">
              <p class="text-gray-500 text-theme-sm dark:text-gray-400">{{ stditem.qty }}</p>
            </td>
          </tr>
        </tbody>

        <div class="relative w-3/4 mb-4">
    <!-- Input field -->
    <input
        v-model="tipeprinter"
        type="text"
        placeholder="Item"
        class="h-11 w-full rounded-lg border border-gray-300 bg-transparent px-4 pr-10 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30"
        @keydown.enter="searchTipePrinter"
    />

    <!-- Search button -->
    <button
        type="button"
        @click="toggleSearchTipePrinter"
        class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-brand-600"
    >
        <svg
        xmlns="http://www.w3.org/2000/svg"
        class="h-5 w-5"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        >
        <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M21 21l-4.35-4.35M11 18a7 7 0 100-14 7 7 0 000 14z"
        />
        </svg>
    </button>

    <div
        v-if="showTipePrinter"
        class="absolute left-0 z-50 mt-1 w-full max-h-48 overflow-auto rounded-md border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800"
    >
        <ul>
        <li
            v-for="(item, index) in filteredTipePrinters"
            :key="index"
            @click="selectTipePrinter(item)"
            class="cursor-pointer px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700"
        >
            {{ item }}
        </li>

        <li
            v-if="filteredTipePrinters.length === 0"
            class="px-3 py-2 text-sm text-gray-500 dark:text-gray-400"
        >
            Tipe Printer Tidak Ditemukan
        </li>
        </ul>
    </div>
    </div>
    </table>

    <div class="flex items-center gap-3 px-2 mt-6">
      <button
        @click="isProfileInfoModal = false"
        type="button"
        class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto"
      >
        Batal
      </button>
      <button
        @click="saveProfile"
        type="button"
        class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto"
      >
        Simpan
      </button>
      <button
        @click="saveProfile"
        type="button"
        class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto"
      >
        Validasi
      </button>
    </div>
      



    </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const tabs = ['General', 'Items']
const activeTab = ref('General')


const stditems = ref([
  {
    nama: 'HP LJ PRO200 M201',
    kategori: 'Printer',
    qty: '4',
  },
  {
    nama: 'LQ2180 PITA EPSON',
    kategori: 'Toner',
    qty: '7',
  },


])
</script>