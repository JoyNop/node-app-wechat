<template>
<div class="information">
  <Header 
  :is-left="true"
  title="详细资料"
  btn_icon="ellipsis-h"/>
</div>
</template>

<script>
import Header from "../components/Header";

export default {
  name: "information",
  computed: {},
  methods: {},
  components: {
    Header
  }
};
</script>

<style>
</style>
