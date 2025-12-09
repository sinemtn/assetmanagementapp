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

    
    <div class="mt-6">

      <!-- Tab Content General -->
      <div v-if="activeTab === 'General'" class="text-gray-800 dark:text-gray-200">

      <!-- No Komplain -->
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
          No. MP
      </label>
      <div class="w-full mb-4" >
        
          <input
              type="text"
              value="test no MP. 058/KLL/exmple"
              class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
          />
      </div>
      <!-- End of No Komplain -->

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

      <!-- Status -->
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
          Status
      </label>
      <div class="w-full mb-4" >
      <select
          class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800"
          >
          <option value=""> KSO </option>
          <option value=""> Beli Putus </option>
      </select>
      </div>
      <!-- End of Status -->

      <!-- Keterangan -->
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
          Keterangan
      </label>
      <div class="w-full mb-4">
      <textarea
          type="text"
          value=""
          class="dark:bg-dark-900 h-20 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
      />
      </div>
      <!-- End of Keterangan -->

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
      </div>
      
      </div>

      <!-- Tab Content Surat Tugas -->
      <div v-else-if="activeTab === 'Surat Tugas'" class="text-gray-800 dark:text-gray-200 ">

        <div class="flex justify-end items-center ">
          <button class="flex items-center rounded-xl border border-gray-200 bg-white p-3 dark:border-gray-800 dark:bg-white/[0.03] md:p-4 hover:shadow-sm transition">
            <PlusIcon/>
            Tambah  
          </button>
        </div>
    

        <table class="min-w-full">
        <thead>
          <tr class="border-b border-gray-200 dark:border-gray-700">
            <th class="px-5 py-3 text-left w-4/16 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Nama File</p>
            </th>
            <th class="px-5 py-3 text-left w-3/16 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Jenis File</p>
            </th>
            <th class="px-5 py-3 text-left w-5/16 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Keterangan</p>
            </th>
            <th class="px-5 py-3 text-left w-3/16 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Aksi</p>
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
              <p class="text-gray-500 text-theme-sm dark:text-gray-400" >{{ stditem.namafile }}</p>
            </td>
            <td class="px-5 py-4 sm:px-6">
              <p class="text-gray-500 text-theme-sm dark:text-gray-400">{{ stditem.jenisfile }}</p>
            </td>
            <td class="px-5 py-4 sm:px-6">
              <p class="text-gray-500 text-theme-sm dark:text-gray-400">{{ stditem.keterangan }}</p>
            </td>
            <td>
              <div class="flex items-center gap-2">
                <button class="edit-button">
                  <router-link to="/complaint/detail" class="block">
                    <span class="flex items-center gap-2">
                      <DeleteIcon/>
                      Hapus
                      </span>
                  </router-link>
                </button>
              </div>
            </td>
          </tr>
        </tbody>
        </table>
      </div>

    </div>




  </div>
</template>

<script setup>
import DeleteIcon from '@/icons/DeleteIcon.vue'
import PlusIcon from '@/icons/PlusIcon.vue'
import { ref } from 'vue'

const tabs = ['General', 'Surat Tugas' ]
const activeTab = ref('General')


const stditems = ref([
  {
    namafile: 'HP LJ PRO200 M201',
    jenisfile: 'Printer',
    keterangan: 'Keterangan mengenai HP LJ PRO2000',
  },
  {
    namafile: 'LQ2180 PITA EPSON',
    jenisfile: 'Toner',
    keterangan: 'Keterangan Mengenai LQ2180',
  },


])
</script>