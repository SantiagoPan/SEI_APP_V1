import React from 'react'

const Login = React.lazy(() => import('./views/pages/login/Login'))
const Message = React.lazy(() => import('./views/Message/Message'))
const MyData = React.lazy(() => import('./views/MyData/MyData'))
const MyOrders = React.lazy(() => import('./views/MyOrders/MyOrders'))
const OfferService = React.lazy(() => import('./views/offerservice/OfferService'))
const OfferProduct = React.lazy(() => import('./views/offerproduct/OfferProduct'))
const HireService = React.lazy(() => import('./views/hireservice/HireService'))
const searchproduct = React.lazy(() => import('./views/searchProduct/searchProduct'))
const Billing = React.lazy(() => import('./views/Billing/billing'))
const registerBank = React.lazy(() => import('./views/registerBank/registerBank'))
const post = React.lazy(() => import('./views/post/post'))


const routes = [
  { path: '/', exact: true, name: 'Home' },
  { path: '/login', name: 'Login', element: Login },
  { path: '/post', name: 'post', element: post, exact: true },
  { path: '/post/post', name: 'post', element: post },
  { path: '/hireservice', name: 'hireservice', element: HireService, exact: true },
  { path: '/hireservicee/hireservice', name: 'HireService', element: HireService },
  { path: '/offerservice', name: 'offerservice', element: OfferService, exact: true  },
  { path: '/offerservice/offerservice', name: 'OfferService', element: OfferService },
  { path: '/offerproduct', name: 'offerproduct', element: OfferProduct, exact: true },
  { path: '/offerproduct/offerproduct', name: 'OfferProduct', element: OfferProduct },
  { path: '/billing', name: 'billing', element: Billing, exact: true },
  { path: '/billing/billing', name: 'Billing', element: HireService },
  { path: '/registerBank', name: 'registerBank', element: registerBank, exact: true },
  { path: '/registerBank/registerBank', name: 'registerBank', element: registerBank },
  { path: '/searchProduct', name: 'searchProduct', element: searchproduct, exact: true },
  { path: '/message', name: 'Message', element: Message, exact: true },
  { path: '/message/message', name: 'Message', element: Message },
  { path: '/mydata', name: 'MyData', element: MyData, exact: true  },
  { path: '/mydata/mydata', name: 'mydata', element: MyData },
  { path: '/MyOrders', name: 'MyOrders', element: MyOrders, exact: true },
  { path: '/MyOrders/MyOrders', name: 'MyOrders', element: MyData },
   
]

export default routes
