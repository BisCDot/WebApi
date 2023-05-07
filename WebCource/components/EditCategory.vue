<template>
  <div class="modal-vue">
    <div class="modal" >
      <h3>{{title}}</h3>
      <form ref="form">
        <div v-if="showInputId">
          <b-form-group
            label="Id"
            label-for="name-input"
            invalid-feedback="ID is required"
          >
            <b-form-input
              id="name-input"
              v-model="categories.id"
            ></b-form-input>
          </b-form-group>
        </div>

      <b-form-group
        label="Name"
        label-for="name-input"
        invalid-feedback="Name is required"
      >
        <b-form-input
          id="name-input"
          v-model="categories.name"
          required
        ></b-form-input>
      </b-form-group>
      </form>
      <div class="d-grid gap-2 d-md-flex justify-content-md-end">
        <button class="btn btn-primary me-md-2" type="button" @click="save">Ok</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "EditCategory",
  async created() {
   await this.getDetail()
  },
  props: {
    id : {
      type : Number,
      default : 0
    },
    title : {
      type : String,
      default: ""
    }
  },
  data(){
    return {
      categories : {}
    }
  },
  emits : ['Save'],
  methods : {
    closeModal(){
        this.showModal = !this.showModal;
    },
    save(Category){
      this.$emit('Save',Category)
    },
    async getCategory() {
      let result = await this.$axios.$get('/api/Category/GetAll');
      this.Categories = result.result
    },
    async getDetail(){
      if(this.id > 0){
        let value =  await this.$axios.$get(`/api/Category/GetById/?id= ${this.id}`)
        this.categories = value.result;
      }else {
        this.categories = {}
      }
    }
  }
}
</script>

<style scoped>
.modal{
  display: block;
}
.modal-vue .overlay {
  position: fixed;
  z-index: 998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, .5);
}

.modal-vue .modal {
  position: relative;
  width: 300px;
  height: 300px;
  z-index: 999;
  padding: 20px 30px;
  margin: 0 auto;
  flex-direction: column;
  background-color: #fff;
  border-radius: 0.3rem;
  background-clip: padding-box;
  border: 1px solid rgba(0, 0, 0, 0.2);
}

.modal-vue .close{
  position: absolute;
  top: 10px;
  right: 10px;
}
</style>
