import React from 'react'
import CIcon from '@coreui/icons-react'
import {
  cilLibraryAdd,
  cilSwapHorizontal,
  cilEnvelopeClosed,
  cilDescription,
  cilAddressBook,
  cilStar,
  cilCart,
} from '@coreui/icons'
import { CNavGroup, CNavItem, CNavTitle } from '@coreui/react'

const _nav = [
  {
    component: CNavItem,
    name: 'Publicar',
    to: '/post',
    icon: <CIcon icon={cilLibraryAdd} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Contratar Servicio',
    to: '/HireService',
    icon: <CIcon icon={cilSwapHorizontal} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Comprar Producto',
    to: '/searchProduct',
    icon: <CIcon icon={cilCart} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Facturación',
    to: '/Billing',
    icon: <CIcon icon={cilDescription} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Mensajes',
    to: '/Message',
    icon: <CIcon icon={cilEnvelopeClosed} customClassName="nav-icon" />,
  },
  {
    component: CNavItem,
    name: 'Mis Datos',
    to: '/MyData',
    icon: <CIcon icon={cilAddressBook} customClassName="nav-icon" />,
  },]

export default _nav
