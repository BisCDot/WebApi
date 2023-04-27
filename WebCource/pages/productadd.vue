<template>
  <div class="main-product-add">
    <form>
      <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label" >Title</label>
        <input type="text" class="form-control" id="exampleFormControlInput1" placeholder="Tiêu đề" v-model="Title">
      </div>
      <div class="mb-3">
        <label for="exampleFormControlTextarea1" class="form-label" >Mô tả</label>
        <textarea class="form-control" id="exampleFormControlTextarea1" rows="3" v-model="Decription"></textarea>
      </div>
      <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label" >Giá</label>
        <input type="text" class="form-control" id="exampleFormControlInput1" placeholder="Giá" v-model="Price">
      </div>
      <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label" >Link Images</label>
        <input type="text" class="form-control" id="exampleFormControlInput1" placeholder="link" v-model="Image">
      </div>
      <select class="form-select form-select-sm" aria-label=".form-select-sm example"  v-model="Category">
        <option selected>Chọn danh mục</option>
        <option v-for="item in CategoryItem" :value="item.id">{{item.name}}</option>
      </select>
      <button class="btn btn-primary" type="submit" @click="addProduct">thêm</button>
    </form>
  </div>

</template>

<script>
export default {
  name: "productadd.vue",
  layout: 'default',
  data(){
    return{
      Title : "",
      Decription: "",
      Price : 0,
      Image : "",
      Category : 0,
      CategoryItem : null
    }
  },
  async created() {
     await this.GetCategory();
  },
  methods : {
    async addProduct(){
        await  this.$axios.post("/api/Cource/Add",{
            title : this.Title,
            description : this.Decription,
            image : this.Image,
            price : this.Price,
            categorysId : this.Category
        })
    },
    async GetCategory(){
      var result = await this.$axios.$get('/api/Category/GetAll');
      this.CategoryItem = result.result;
    },
  }
}
</script>

<style scoped>
.main-product-add{
  padding: 50px;
  display: block;
}
</style>
