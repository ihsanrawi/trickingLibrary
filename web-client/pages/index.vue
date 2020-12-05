<template>
  <div>

    <div v-if="tricks">
      <div v-for="trick in tricks">
        <v-btn :to="`/tricks/${trick.id}`">{{trick.name}}</v-btn>
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
