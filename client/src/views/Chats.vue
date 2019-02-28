<template>
  <div class="chat">
    <Header title="微信" btn_icon="plus"/>
    <div class="container">
      <UserCell 
      v-for="chatItem in chatDataList" 
      :key="chatItem._id" 
      :user="chatItem.target"
      :count="chatItem.count"
      @click="$router.push({name:'chat',params:{user:chatItem.target}})"/>
    </div>
  </div>
</template>

<script>
import Header from "../components/Header";
import UserCell from "../components/UserCell";

export default {
  name: "chats",
  components: {
    Header,
    UserCell
  },
  data() {
    return {
      chatDataList: []
    };
  },
  computed: {
    user() {
      return this.$store.getters.user;
    }
  },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      vm.$axios(`/api/profile/msg/${vm.user.id}`).then(res => {
        console.log(res.data);
        vm.chatDataList = res.data;
      });
    });
  }
};
</script>
<style scoped>
.chat {
  width: 100%;
  height: 100%;
  overflow: hidden;
}
.container {
  width: 100%;
  height: calc(100% - 50px);
  margin-top: 50px;
  overflow: auto;
}
</style>