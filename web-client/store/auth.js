const initState = () => ({
  user: null,
  profile: null,
  loading: true
})

export const state = initState

const ROLES = {
  MODERATOR: "Mod"
}

export const getters = {
  authenticated: state => !state.loading && state.user != null,
  moderator: (state, getters) => getters.authenticated && state.user.profile.role === ROLES.MODERATOR
}

export const mutations = {
  saveUser(state, {user}) {
    state.user = user
  },
  saveProfile(state, {profile}) {
    state.profile = profile
  },
  finish(state) {
    state.loading = false
  }
}

export const actions = {
  initialize({commit}) {
    return this.$auth.querySessionStatus()
      .then(sessionStatus => {
        console.log(sessionStatus)
        if (sessionStatus){
          return this.$auth.getUser()
        }
      })
      .then(async user => {
        if (user) {
          commit('saveUser', {user})
          this.$axios.setToken(`Bearer ${user.access_token}`)
          const profile = await this.$axios.get('/api/users/me')
          commit('saveProfile', {profile})
        }
      })
      .catch(err => {
        console.log(err.message)
        if(err.message === "login_required"){
          //clean up here
          return this.$auth.removeUser();
        }
      })
      .finally(() => commit('finish'))
  },
  _watchUserLoaded({state, getters}, action) {
    return new Promise((resolve, reject) => {
      if(state.loading) {
        console.log("start watching")
        const unwatch = this.watch(
          (store) => store.auth.loading,
          (newValue, oldValue) => {
            if(newValue) {
              unwatch();

              if(!getters.authenticated) {
                this.$auth.signinRedirect()
              } else if(!newValue) {
                console.log("user finish loading executing action")
                resolve(action())
              }
            }
          }
        )
      } else {
        console.log("user finish loading executing action")
        resolve(action())
      }
    })
  },
}