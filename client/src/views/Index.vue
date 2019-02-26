<template>
<div class="index">
  <router-view></router-view>主页99999999999999999999999
  <TabBar />
</div>
</template>

<script>
import TabBar from "../components/TabBar";
export default {
  name: "index",
  components: {
    TabBar
  }
};
</script>

<style scoped>
.index {
  width: 100%;
  height: 100%;
}
</style>
