import apiUrl from '@/constants/api'
export default  {
  state : ()=>({
      categorries : {}
  }),
  mutations : {
      SET_CATEGORY : (state,data) => {
        state.categorries = data
      }
  },
  actions : {
    getCategory({commit},data){
      return new Promise((resolve, reject) => {
        return this.$axios.$get(`${apiUrl.ORGANIZATION_CATEGORY}GETALL`)
          .then((response) => {
            commit('SET_CATEGORY',response.data.result);
            return resolve(response.data.result);
          }).catch((err) => {
            commit('SET_CATEGORY',null);
            return reject(err);
          })
      })
    },

  }
}
