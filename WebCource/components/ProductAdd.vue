<template>
  <div>
    <b-modal
      v-model="modalShow"
      id="modal-prevent-closing"
      ref="modal"
      :title="Title"
      @ok="Save"
    >
      <div class="main-product-add">
        <form>
          <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label" >Title</label>
            <input type="text" class="form-control" id="exampleFormControlInput1" placeholder="Tiêu đề" v-model="Product.Title">
          </div>
          <div class="mb-3">
            <label for="exampleFormControlTextarea1" class="form-label" >Mô tả</label>
            <textarea placeholder="Mô tả" class="form-control" id="exampleFormControlTextarea1" rows="3" v-model="Product.Description"></textarea>
          </div>
          <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label" >Giá</label>
            <input type="text" class="form-control" id="exampleFormControlInput1" placeholder="Giá" v-model="Product.Price">
          </div>
          <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label" >Link Images</label>
            <input type="text" class="form-control" id="exampleFormControlInput1" placeholder="link" v-model="Product.Image">
          </div>
          <select class="form-select form-select-sm" aria-label=".form-select-sm example"  v-model="Product.CategoryId" >
            <option selected>Chọn danh mục</option>
            <option v-for="item in CategoryItem" :value="item.id">
              {{item.name}}
            </option>
          </select>
        </form>
        <div class="d-grid gap-2 d-md-flex justify-content-md-end">
          <button class="btn btn-primary me-md-2" type="button" @click="addproduct">thêm</button>
        </div>
      </div>
    </b-modal>
  </div>

</template>

<script>
export default {
  name: "ProductAdd",
  async created() {
    await this.getCategory()
  },
  data(){
    return {
      Product : {
        Title : "",
        Description : "",
        Price : "",
        Image : "",
        CategoryId : ""
      },
      CategoryItem : null
    }
  },
  props : {
     Title: String,
    modalShow: Boolean
  },
  emits : ['Save','addproduct'],
  methods :{
    Save(e){
      console.log(this.Product)
      this.$emit('Save', { ...this.Product });
    },
    async getCategory(){
      var result = await this.$axios.$get('/api/Category/GetAll');
      this.CategoryItem = result.result
    },
    addproduct(){
      this.$emit("addproduct",this.Product)
    }
  }
}
</script>

<style scoped>

</style>
