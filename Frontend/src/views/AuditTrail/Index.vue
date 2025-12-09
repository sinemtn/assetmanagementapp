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
import Index from "@/components/audittrail/Index.vue";
const currentPageTitle = ref("Audit Trail");
</script>
