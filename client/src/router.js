import Vue from 'vue'
import Router from 'vue-router'
import Index from './views/Index'
// import Login from './views/Login'
// import Register from './views/Register'

Vue.use(Router)

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "index",
      redirect: "/chats",
      component: Index,
      children: [
         
        {
          path: "/chats",
          name: "chats",
          component: () => import('./views/Chats')
        },
        {
          path: "/contacts",
          name: "contacts",
          component: () => import('./views/Contacts')
        },
        {
          path: "/discover",
          name: "discover",
          component: () => import('./views/Discover')
        },
        {
          path: "/me",
          name: "me",
          component: () => import('./views/Me')
        }
      ]
    },
    {
      path: "/login",
      name: "login",
      component: () => import('./views/Login')
    },
    {
      path: "/register",
      name: "register",
      component: () => import('./views/Register')
    },
    {
      path: "/information",
      name: "information",
      component: () => import('./views/Information')
    }
  ]
})

router.beforeEach((to, from, next) => {
  const isLogin = localStorage.wxpyqToken ? true : false;
  if (to.path == "/login" || to.path == "/register") {
    next();
  } else {
    isLogin ? next() : next("/login");
  }
})

export default router
