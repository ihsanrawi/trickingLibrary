<template>
  <div>

    <div v-if="tricks">
      <div v-for="trick in tricks">
        <v-btn class="ma-2" :to="`/trick/${trick.id}`">{{trick.name}}</v-btn>
      </div>
    </div>

  </div>
</template>

<script>
  import {mapState} from 'vuex';

  export default {
    computed: mapState('tricks', ['tricks']),
  }
</script>
