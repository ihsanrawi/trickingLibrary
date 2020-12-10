<template>
  <div>
    <div v-if="item">
      {{item.description}}
    </div>

    <comment-section :comments="comments" @send="send"/>
  </div>
</template>

<script>
import CommentSection from "@/components/comments/comment-section";

const endpointResolver = (type) => {
  if (type === 'trick') return 'tricks'
}

export default {
  components: {CommentSection},
  data: () => ({
    item: null,
    comments: [],
    comment: "",
    replyId: 0,
  }),
  created() {
    const {modId, type, trickId} = this.$route.params
    const endpoint = endpointResolver(type)

    this.$axios.$get(`/api/${endpoint}/${trickId}`)
      .then((item) => this.item = item)

    this.$axios.$get(`/api/moderation-items/${modId}/comments`)
      .then((comments) => this.comments = comments)
  },
  methods: {
    send(content) {
      const {modId} = this.$route.params


      return this.$axios.$post(`/api/moderation-items/${modId}/comments`,
        {content})
        .then((comment) => this.comments.push(comment))
    },
  }
}
</script>

<style scoped>

</style>
