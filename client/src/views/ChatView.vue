<template>
  <div class="chat">
    <Header v-if="targetUser" :is-left="true" :title="targetUser.name" btn_icon="ellipsis-h"/>
  <div class="container">
    <!-- 聊天内容 -->
  </div>
  <div class="footer_wrap">
    <input type="text" v-model="msgValue">
    <button :disabled="msgValue==''" @click="sendMessage">发送</button>
  </div>
  </div>
</template>

<script>
import Header from "../components/Header";

export default {
  name: "chatview",
  computed: {},
  methods: {
    sendMessage(){
      console.log(this.msgValue);
      
    }
  },
  components: {
    Header
  },
  data() {
    return {
      targetUser: null,
      msgValue:""
    };
  },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      vm.targetUser = to.params.user;
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
  height: calc(100% - 100px);
  box-sizing: border-box;
  background-color: #f1f1f1;
  margin-top: 50px;
  padding: 8px;
  overflow-y: scroll;
}
.footer_wrap {
  width: 100%;
  height: 50px;
  box-sizing: border-box;
  border-top: 1px solid #d9d9d9;
  position: absolute;
  bottom: 0;
  padding: 8px;
  background-color: #fafafa;
}
.footer_wrap input {
  width: 70%;
  height: 30px;
  outline: none;
  border: 1px solid #d9d9d9;
  border-radius: 4px;
}
.footer_wrap button {
  width: 20%;
  height: 34px;
  border: 1px solid #d9d9d9;
  border-radius: 4px;
  margin-left: 8px;
  outline: none;
  background-color: #f1f1f1;
}
.footer_wrap button[disabled] {
  background-color: #d9d9d9;
  cursor: not-allowed;
  pointer-events: none;
}

.left_msg {
  justify-content: flex-start;
}
.right_msg {
  justify-content: flex-end;
}
.left_msg,
.right_msg {
  width: 100%;
  display: flex;
  margin: 5px 0;
}
.content_wrap img {
  width: 3rem;
  height: 3rem;
}
.content_wrap span {
  display: inline-block;
  max-width: 65%;
  border: 1px solid #d9d9d9;
  border-radius: 4px;
  margin: 0 5px;
  padding: 8px;
  box-sizing: border-box;
  word-break: break-all;
}
.left_msg span {
  background-color: #fff;
}
.right_msg span {
  background-color: #0fce0d;
}
</style>