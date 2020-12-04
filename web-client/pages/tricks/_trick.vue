<template>
  <div class="d-flex justify-center align-start">
    <div class="mx-2" v-if="submissions">
      <div v-for="i in 20">
        <div v-for="submission in submissions">
          {{submission.id}} - {{submission.description}} - {{submission.trickId}}
          <div>
            <video width="400" controls :src="`http://localhost:5000/api/videos/${submission.video}`"></video>
          </div>
        </div>
      </div>
    </div>

    <div class="mx-2 sticky">
      Trick: {{ $route.params.trick }}
    </div>

  </div>
</template>

<script>
  import {mapState} from 'vuex';

  export default {
    computed: mapState('submissions', ["submissions"]),
    async fetch() {
      const trickId =this.$route.params.trick;
      await this.$store.dispatch("submissions/fetchSubmissionForTrick", {trickId}, {root:true});
    }
  }
</script>

<style scoped>
  .sticky {
    position: -webkit-sticky;
    position: sticky;
    top: 48px;
  }
</style>
