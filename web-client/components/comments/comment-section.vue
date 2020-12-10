<template>
  <div>
    <comment-input label="Comment" @send="(c) => $emit('send', c)"/>

    <v-divider class="my-5"/>

    <comment v-for="comment in comments" :comment="comment" :key="comment.id"/>
  </div>
</template>

<script>
import CommentInput from "@/components/comments/comment-input";
import Comment from "@/components/comments/comment";
export default {
name: "comment-section",
  components: {Comment, CommentInput},
  props:{
    comments: {
      required: true,
      type: Array,
    },
  },
}
</script>

<style scoped>

</style>
