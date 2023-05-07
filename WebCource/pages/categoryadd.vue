<template>
  <div>
    <table class="table">
      <thead>
      <tr>
        <th scope="col">Id</th>
        <th scope="col">Name</th>
        <th scope="col"></th>
        <th scope="col"></th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in itemsCategory">
        <th scope="row">{{item.id}}</th>
        <td>{{item.name}}</td>
        <td>
          <div class="d-grid gap-2 d-md-block">
            <!-- Button trigger modal -->
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="DeleteOk(item.id)">
              X
            </button>
          </div>
        </td>
        <td>
          <div>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="Edit(item.id)">
              Sửa
            </button>
          </div>
        </td>
      </tr>
      </tbody>
    </table>
    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
      <button class="btn btn-primary me-md-2" type="button" @click="() => {addCategoryShow = !addCategoryShow;modalShow = true}">Thêm danh mục</button>
    </div>
    <Transition>
      <EditCategory :show-modal="addCategoryShow" v-if="modalShow" :category="Category" @Save="addCategory" :showInputId="false"></EditCategory>
    </Transition>
  </div>
</template>

<script>

export default {
  name: "categoryadd",
  components: {},
  layout: 'default',
  data(){
    return {
      itemsCategory : null,
      modalShow : true,
      addCategoryShow : false,
      editCategory : false,
      Category : {
        id : 0,
        name : ""
      },
      id : 0
    }
  },
  async created() {
    await this.GetCategory();
  },

  methods :{
    async GetCategory(){
      var result = await this.$axios.$get('/api/Category/GetAll');
      this.itemsCategory = result.result;
    },
    async Edit(id){
        this.modalShow = false
        this.editCategory = !this.editCategory;
        if (id > 0){
          let response = await this.$axios.$get(`/api/Category/GetById/?Id=${id}`);
          let item = response.result
          this.Category.name = item.name
          this.Category.id = item.id
        }

    },
   async addCategory(item){
      await this.$axios.post('/api/Category/AddCategory',{
        name : item.name
      });
      this.addCategoryShow = false;
     await this.GetCategory();
    },
    async DeleteOk(id){
      await  this.$axios.$delete(`/api/Category/Delete/?Id=${id}`);
    },
    async SaveCategory(item){
      console.log(item)
      if(item.id > 0){
        await  this.$axios.post('/api/Category/Save',{
          id : item.id,
          name : item.name
        });
      }
      this.addCategoryShow = !this.addCategoryShow;
      this.modalShow = true;
      this.editCategory = false;
      await this.GetCategory();
    }
  }
}
</script>

<style scoped>

</style>
