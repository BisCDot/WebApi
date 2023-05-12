import apiUrl from "@/constants/api";

export default {
    strict : true,
    state : () => ({
        listCourse  : [],
        course : {},

    }),
    mutations : {
      SET_LIST : (state,data) => {
        state.listCourse = data
      },
      SET_PRODUCT : (state,data) => {
        state.course = data
      },
      SET_PRODUCT_TITLE : (state,data) => {
        state.course.title = data
      },
      SET_PRODUCT_DESCRIPTION : (state, data) => {
        state.course.description = data
      },
      SET_PRODUCT_PRICE : (state,data) => {
        state.course.price = data
      },
      SET_PRODUCT_IMAGE : (state,data) => {
          state.course.image = data
      },
      SET_PRODUCT_CATEGORY : (state,data) => {
          state.course.categoryId = data
      }
    },
    actions : {
      getList({commit},data) {
        return new Promise((resolve, reject) => {
          return this.$axios
            .post(`${apiUrl.ORGANIZATION_COURCE}List`,data)
            .then((response) => {
              commit('SET_LIST', response.data.result);
              return resolve(response.data.result);

            })
            .catch((err) => {
              commit('SET_LIST', null);
              return reject(err)
            })
        })
      },
      getAll({commit},data) {
        return new Promise((resolve, reject) => {
          return this.$axios
            .get(apiUrl.ORGANIZATION_GET_LIST_COURCE)
            .then((response) => {
              commit('SET_LIST', response.data.result);
              return resolve(response.data.result)
            })
            .catch((err) => {
              commit('SET_LIST', null);
              return reject(err)
            })
        })
      },
      delete({commit},data){
        return new Promise((resolve, reject) => {
          return this.$axios.$delete(`${apiUrl.ORGANIZATION_COURCE}Delete/?id=${data}`)
            .then((response) => {
                return resolve(response.result)
            })
            .catch((err) => {
              return reject(err)
            })
        })
      },
      getById({commit},data){
          return new Promise((resolve, reject) => {
            return this.$axios.get(`${apiUrl.ORGANIZATION_COURCE}GetById/?id=${data}`)
              .then((response) => {
                commit('SET_PRODUCT',response.data.result)
                return resolve(response.data.result);
              })
              .catch((err) => {
                commit('SET_PRODUCT',null);
                return reject(err)
              })
          })
      },
      save({commit}, data){
        return new Promise((resolve, reject) => {
          return this.$axios.post(`${apiUrl.ORGANIZATION_COURCE}Save`,data)
            .then((response) => {
              commit('SET_PRODUCT', response.result);
              return resolve(response.result);
            })
            .catch((err) => {
              commit('SET_PRODUCT',null)
              return reject(err)
            })
        })
      },
      add({commit}, data){
        return new Promise((resolve, reject) => {
          return this.$axios.post(`${apiUrl.ORGANIZATION_COURCE}Add`,data)
            .then((response) => {
              commit('SET_PRODUCT',response.result);
              return resolve(response.result)
            })
            .catch((err) => {
              commit('SET_PRODUCT',null)
              return reject(err)
            })
        })
      },

    },
    getters: {
        listCourse : state => state.listCourse,
        course : state => state.course,
    },
}
