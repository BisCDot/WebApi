<template>
  <b-modal
    v-model="modalShow"
    id="modal-prevent-closing"
    ref="modal"
    title="Sửa danh mục "
    @ok="SaveCategory"
  >
    <form ref="form" @submit.stop.prevent="handleSubmit" v-for="item in items" :key="item.id">
      <b-form-group
        label="item"
        label-for="name-input"
        invalid-feedback="Vui lòng nhập dữ liệu"
      >
        <b-form-input
          id="name-input"
          v-model="item.id"
          v-model:value="item.id"
          required
        ></b-form-input>
      </b-form-group>
    </form>
  </b-modal>
</template>
<script>
export default {
  name: "Edit.vue",
  props: ['items','modalShow'],
  emits : ['SaveCategory']
}
</script>

<style scoped>

</style>
