<template>
  <button @click="SearchValue">Search</button>
  <test />
</template>

<script setup>
import test from "@/components/test.vue";
import { useStore } from "vuex";

const store = useStore();

function SearchValue() {
  store.commit("app/SET_SEARCH", String(Date.now()));
}
</script>
