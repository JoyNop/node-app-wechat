<template>
  <div id="app">
    <router-view/>
  </div>
</template>
<script>
import jwt_decode from "jwt-decode";
export default {
  name:'app',
  created(){
    if(localStorage.wxpyqToken){
      const decode=jwt_decode(localStorage.wxpyqToken);
      this.$store.dispatch("setUser",decode)
    }
  }
}
</script>

<style>
html,
body,
#app {
  width: 100%;
  height: 100%;
}
</style>

 