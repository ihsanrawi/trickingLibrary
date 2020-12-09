<template>
  <div>
    <v-btn :to="`/moderation/${item.type}/${item.target}`" v-for="item in items" :key="item.id">
      {{item.target}}
    </v-btn>
  </div>
</template>

<script>
  export default {
    data:() => ({
      items: [],
    }),
    async fetch() {
      this.items = await this.$axios.$get("/api/moderation-items")
    }
  }
</script>

<style scoped>

</style>
