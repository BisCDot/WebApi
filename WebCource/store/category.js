import apiUrl from '@/constants/api'
export default  {
  state : ()=>({
      listCategory : [],
      category: {}
  }),
  mutations : {
      SET_CATEGORY : (state,data) => {
        state.category = data
      },
      SET_LIST_CATEGORY : (state,data) => {
        state.listCategory = data
      },
      SET_NAME_CATEGORY : (state,data) => {
        state.category.name = data
      }
  },
  actions : {
    getCategory({commit},data){
      return new Promise((resolve, reject) => {
        return this.$axios.$get(`${apiUrl.ORGANIZATION_CATEGORY}GETALL`)
          .then((response) => {
            commit('SET_LIST_CATEGORY',response.result);
            return resolve(response.result);
          }).catch((err) => {
            commit('SET_LIST_CATEGORY',null);
            return reject(err);
          })
      })
    },
    getById({commit},data){
      return new Promise((resolve, reject) => {
        return this.$axios.get(`${apiUrl.ORGANIZATION_CATEGORY}GetById/?id=${data}`)
          .then((response) => {
            commit('SET_CATEGORY',response.data.result)
            return resolve(response.data.result);
          })
          .catch((err) => {
            commit('SET_CATEGORY',null);
            return reject(err)
          })
      })
    },
    delete({commit},data){
      return new Promise((resolve, reject) => {
        return this.$axios.$delete(`${apiUrl.ORGANIZATION_CATEGORY}Delete/?id=${data}`)
          .then((response) => {
            return resolve(response.result)
          })
          .catch((err) => {
            return reject(err)
          })
      })
    },
    save({commit}, data){
      return new Promise((resolve, reject) => {
        return this.$axios.post(`${apiUrl.ORGANIZATION_CATEGORY}Save`,data)
          .then((response) => {
            commit('SET_CATEGORY', response.result);
            return resolve(response.result);
          })
          .catch((err) => {
            commit('SET_CATEGORY',null)
            return reject(err)
          })
      })
    },
    add({commit}, data){
      return new Promise((resolve, reject) => {
        return this.$axios.post(`${apiUrl.ORGANIZATION_CATEGORY}AddCategory`,data)
          .then((response) => {
            commit('SET_CATEGORY',response.result);
            return resolve(response.result)
          })
          .catch((err) => {
            commit('SET_CATEGORY',null)
            return reject(err)
          })
      })
    },
  },
  getters :{
    category : state =>  state.category,
    listCategory : state => state.listCategory

  }
}
