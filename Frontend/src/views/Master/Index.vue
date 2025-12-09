<template>
  <AdminLayout>
    <PageBreadcrumb :pageTitle="currentPageTitle" />
    
    <div class="grid grid-cols-12 gap-4 md:gap-6">
      <div class="col-span-12">
        <Index/>
      </div>
    </div>
       
   
  </AdminLayout>
</template>

<script setup>
import { ref } from "vue";
import PageBreadcrumb from "@/components/common/PageBreadcrumb.vue";
import AdminLayout from "@/components/layout/AdminLayout.vue";
import ComponentCard from "@/components/common/ComponentCard.vue";
import Index from "@/components/master/Index.vue";
import EcommerceMetrics from "@/components/ecommerce/EcommerceMetrics.vue";
const currentPageTitle = ref("Master Data");
</script>
