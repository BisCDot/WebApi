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
            <!--            <b-modal v-model="modalShow" title="Xóa" @ok="DeleteOk">Bạn có chắc muốn xóa item này không ?</b-modal>-->
          </div>
        </td>
        <td>
          <div>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="Edit(item)">
              Sửa
            </button>
              <Edit :item="updateCategory"
                    :modal-show="modalShow"
                    @save-category="SaveCategory">
              </Edit>
          </div>
        </td>
      </tr>
      </tbody>
    </table>
    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
      <button class="btn btn-primary me-md-2" type="button" @click="addCategoryShow = !addCategoryShow">Thêm danh mục</button>
      <b-modal
        v-model="addCategoryShow"
        id="modal-prevent-closing"
        ref="modal"
        title="thêm danh mục "
        @ok="addCategory"
      >
        <form ref="form" @submit.stop.prevent="handleSubmit">
          <b-form-group
            label="Name"
            label-for="name-input"
            invalid-feedback="Name is required"
          >
            <b-form-input
              id="name-input"
              v-model="Category.name"
              required
            ></b-form-input>
          </b-form-group>
        </form>
      </b-modal>
    </div>
  </div>
</template>

<script>
import Edit from '@/components/Edit.vue'

export default {
  name: "categoryadd",
  components: {Edit},
  layout: 'default',
  data(){
    return {
      itemsCategory : null,
      modalShow : false,
      addCategoryShow : false,
      updateCategory : {
        id : 0,
        name : ""
      },
      Category : {
        id : 0,
        name : ""
      }
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
    async Edit(item){
        this.modalShow = !this.modalShow;
        this.updateCategory.name = item.name
        this.updateCategory.id = item.id
    },
   async addCategory(){
      await this.$axios.post('/api/Category/AddCategory',{
        name : this.Category.name
      });
      this.GetCategory();
    },
    async DeleteOk(id){
      await  this.$axios.$delete(`/api/Category/Delete/?Id=${id}`);
    },
    async SaveCategory(){
      await  this.$axios.post('/api/Category/Save',{
        id : this.updateCategory.id,
        name : this.updateCategory.name
      })
    }
  }
}
</script>

<style scoped>

</style>
