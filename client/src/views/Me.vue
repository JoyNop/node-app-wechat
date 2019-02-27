<template>
  <div>
    <Header title="我"/>
    <div class="container">
      <InfoCell :userInfo="user"/>
      <div class="btn_wrapper">
        <YButton @click="loginOut">退出登录</YButton>
      </div>
    </div>
  </div>
</template>

<script>
import YButton from "../components/YButton";

import Header from "../components/Header";
import InfoCell from "../components/InfoCell";
export default {
  name: "me",
  components: {
    Header,
    InfoCell,
    YButton
  },
  methods: {
    loginOut() {
      localStorage.removeItem("wxpyqToken");
      this.$router.push("/login");
    }
  },
  computed: {
    user() {
      return this.$store.getters.user;
    }
  }
};
</script>


<style scoped>
.mine {
  width: 100%;
  height: 100%;
  overflow: hidden;
}
.container {
  width: 100%;
  height: calc(100% - 50px);
  margin-top: 50px;
  box-sizing: border-box;
  padding: 16px 0;
  /* background-color: #f1f1f1; */
}

.btn_wrapper {
  box-sizing: border-box;
  padding: 20px;
}
</style>