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
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="deleteOk(item.id)">
              X
            </button>
          </div>
        </td>
        <td>
          <div>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="edit(item.id)">
              Sửa
            </button>
          </div>
        </td>
      </tr>
      </tbody>
    </table>
    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
      <button class="btn btn-primary me-md-2" type="button" @click="()=> {modalShow = !modalShow; id = 0;showIdInputEdit = false; editTitleCategory ='Thêm danh mục'}">Thêm danh mục</button>
    </div>
    <Transition>
      <EditCategory @ok="editOk" :title="editTitleCategory" v-if="modalShow" :id="id" :showInputId="showIdInputEdit" @closeModal="modalShow = false"></EditCategory>
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
      modalShow : false,
      showIdInputEdit : false,
      id : 0,
      editTitleCategory : "",
    }
  },
  async created() {
    await this.getCategory();
  },

  methods :{
    async getCategory(){
      await this.$store.dispatch('category/getCategory');
    },
   async editOk(){
      await this.getCategory();
      this.modalShow = false;
    },
    edit(id){
        this.editTitleCategory = "Sửa danh mục";
        this.modalShow = !this.modalShow;
        this.showIdInputEdit = true
        this.id = id;
    },
    async deleteOk(id){
      await  this.$store.dispatch('category/delete',id);
      await this.getCategory();
    },

  },
  computed : {
    itemsCategory(){
      return this.$store.getters['category/listCategory']
    }
  }
}
</script>

<style scoped>

</style>
