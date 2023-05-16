<template>
  <div>
    <div class="filter">
      <div class="search">
        <div  class="input-search" >
          <input
            placeholder="Tìm kiếm sản phẩm..."
            spellcheck="false"
            v-model="valueSearch"
            id="queryFind"
            v-on:keyup.enter="searchProduct"
          />
          <span class="bi-search" @click="searchProduct">
            <div class="bi-search-icon"></div>
          </span>
        </div>
      </div>
      <div class="select-category">
        <select class="form-select" aria-label="Default select example" v-model="pagingParamFilter.categoryId">
          <option selected value="0">lọc danh mục</option>
          <option v-for="item in category" :value="item.id">{{item.name}}</option>
        </select>
      </div>
      <div class="filter-price">
        <span class="filter-title">Giá tiền</span>
        <range-slider
          class="slider"
          :min="minPrice"
          max="10000000"
          step="10"
          v-model="maxPrice"
          @change="filterPrice"
        >
        </range-slider>
        <div class="filter-price-input">
          <label class="name-input">Từ</label>
          <label class="input-name">Đến</label>
          <input type="number" v-model="pagingParamFilter.minPrice">
          <input type="text" v-model="maxPrice">
        </div>
      </div>
    </div>

    <table class="table">

      <thead class="table-dark">
      <tr>
        <th scope="col">Id</th>
        <th scope="col">title</th>
        <th scope="col">Description</th>
        <th scope="col">Image</th>
        <th scope="col">Price</th>
        <th scope="col">Danh Mục</th>
        <th scope="col"></th>
        <th scope="col"></th>
      </tr>
      </thead>
      <tr v-for="item in cource.data">
        <td>{{ item.id }}</td>
        <td>{{ item.title }}</td>
        <td>{{ item.description }}</td>
        <td><img class="image-list" :src="item.image"></td>
        <td>{{ item.price }}</td>
        <td>{{item.categoryName}}</td>
        <td>
          <div class="d-grid gap-2 d-md-block">
            <!-- Button trigger modal -->
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"
                    @click="deleteOk(item.id)">
              X
            </button>
          </div>
        </td>
        <td>
          <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"
                  @click="editProduct(item.id)">
            Sửa
          </button>
        </td>
      </tr>
    </table>
    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
      <button class="btn btn-primary me-md-2" type="button" @click="()=>{modalShow = true; id = 0; showIdEditProduct = false; TitleEditProduct= 'Thêm sản phẩm'}">Thêm sản
        phẩm
      </button>
    </div>
    <Transition>
      <EditProduct @save="saveEdit" :title="TitleEditProduct" :id="id" :show-id-input="showIdEditProduct" v-if="modalShow" @closeModal="modalShow = false"></EditProduct>
    </Transition>
    <div class="overflow-auto">
      <b-pagination
        @page-click="paginationClick"
        v-model="pagingParamFilter.pageIndex"
        :total-rows="cource.totalRows"
        :per-page="pagingParamFilter.pageSize"
        aria-controls="my-table"
      ></b-pagination>
    </div>
  </div>
</template>

<script>
import 'vue-range-slider/dist/vue-range-slider.css'
export default {
  name: "ListCource.vue",
  data() {
    return {
      showIdEditProduct : false,
      modalShow: false,
      categories: null,
      minPrice : 0,
      maxPrice : 0,
      valueSearch : "",
      id: 0,
      TitleEditProduct : "",
      pagingParamFilter : {
        sortExpression: "",
        pageSize: 2,
        pageIndex: 1,
        skip: 0,
        notSkip: 0,
        minPrice: 0,
        maxPrice: 0,
        categoryId : 0,
        status: 0,
        keyWord: ""
      }
    }
  },
  async created() {
    await this.getList();
    await this.getCategory();
  },
  watch:{
    'pagingParamFilter.categoryId'(){
      this.pagingParamFilter.pageIndex = 1
      this.getList();
    }
  },
  methods: {
    // filterCategory(e){
    //   this.pagingParamFilter.categoryId = e.target.value || 0
    //   this.pagingParamFilter.pageIndex = 1
    //   this.getList();
    // },
    async searchProduct(){
      this.pagingParamFilter.keyWord = this.valueSearch
      this.pagingParamFilter.pageIndex = 1
      this.getList();
    },
    filterPrice() {
      this.pagingParamFilter.maxPrice = this.maxPrice
      this.pagingParamFilter.minPrice = this.minPrice
      this.pagingParamFilter.pageIndex = 1
      this.getList();
    },
    paginationClick(event, pageNumber){
      this.pagingParamFilter.pageIndex = pageNumber
      this.getList();
    },
    getList() {
      this.$store.dispatch('product/getList',this.pagingParamFilter)
    },
    async deleteOk(id) {
      await this.$store.dispatch('product/delete',id)
      await this.getList();
    },
    async getCategory(){
      await  this.$store.dispatch('category/getCategory')
    },
    async saveEdit(){
      await this.getList();
      this.modalShow = false
    },
    editProduct(id) {
      this.TitleEditProduct = 'Sửa sản phẩm'
      this.showIdEditProduct = true;
      this.id= id;
      this.modalShow = true;
    },
  },
  computed : {
      cource(){
        return this.$store.getters['product/listCourse']
      },
      category(){
        return this.$store.getters['category/listCategory']
      }
  }
}
</script>

<style scoped>
.image-list {
  height: 50px;
  width: 50px;
}
.overflow-auto{
}
.slider{
  float: right;
  width: 200px;
  margin-bottom: 73px;
}
.filter{
  height: 60px;
}
.search {
  display: flex;
  margin: 0px;
  height: 100%;
  float: left;
  outline: none;
}

.search .input-search {
  margin: auto;
  width: 500px;
  height: 35px;
  display: flex;
  border: 1px solid #7f7f7f;
  background-color: white;
  justify-content: space-between;
  overflow: hidden;
  transition: all 0.2s ease;
  outline: 3px solid transparent;
}

.search .input-search:focus-within {
}

.search .input-search span {
  height: 100%;
  display: flex;
  align-items: center;
}

.search .input-search .bi-search {
  padding-right: 1rem;
  margin-left: 10px;
  cursor: pointer;
  padding-left: 1rem;
  background: url('@/assets/icon/search.svg');
  background-position: 3px 10px;
  background-repeat: no-repeat;
}
.bi-search-icon {

}

.search .input-search .bi-search:hover {
  color: #2d4bf0;
}
.search .input-search input {
  padding: 0 0.5rem 0 1rem;
  width: 100%;
  font-size: 15px;
  color: #1d1d1d;
  border: 1px solid black;
}
.filter-price {
  position: relative;
}
.filter-title{
  position: absolute;
  bottom: 20px;
  margin-right: 207px;
  right: 0;
  font-weight: 600;
}
.filter-price-input{
  position: relative;
}
.filter-price-input .name-input {
  float: right;
  font-weight: 500;
  margin-top: 26px;
}
.filter-price-input .input-name{
  position: absolute;
  font-weight: 500;
  right: 0;
  margin-top: 25px;
  padding-right: 60px;
}
.filter-price-input > input[type="text"]{
  position: absolute;
  right: 0;
  width: 100px;
  margin-top: 60px;
}
.filter-price-input > input[type="number"]{
  position: absolute;
  width: 100px;
  right: 167px;
  margin-top: 60px;
}
.select-category{
  display: inline-flex;
}
.select-category > select{
  margin-top: 20px;
  width: 124px;
  margin-left: 100px;
}
</style>
