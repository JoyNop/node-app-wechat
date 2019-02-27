<template>
  <div class="chat">
    <Header v-if="targetUser" :is-left="true" :title="targetUser.name" btn_icon="ellipsis-h"/>
  </div>
</template>

<script>
import Header from "../components/Header";

export default {
  name: "chatview",
  computed: {},
  methods: {},
  components: {
    Header
  },
  data() {
    return {
      targetUser: null
    };
  },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      vm.targetUser = to.params.user;
    });
  }
};
</script>

<style>
</style>