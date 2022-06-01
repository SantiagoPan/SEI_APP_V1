import React from 'react'
import {
  CAvatar,
  CBadge,
  CDropdown,
  CDropdownDivider,
  CDropdownHeader,
  CDropdownItem,
  CDropdownMenu,
  CDropdownToggle,
} from '@coreui/react'
import {
  cilBell,
  cilCreditCard,
  cilCommentSquare,
  cilEnvelopeOpen,
  cilMoney,
  cilFile,
  cilLockLocked,
  cilSettings,
  cilExternalLink,
  cilTask,
  cilUser,
} from '@coreui/icons'
import CIcon from '@coreui/icons-react'

import avatar8 from './../../assets/images/avatars/profile.png'

const AppHeaderDropdown = () => {

  return (
    <CDropdown variant="nav-item">
      <CDropdownToggle placement="bottom-end" className="py-0" caret={false}>
        <CAvatar src={avatar8} size="md" />
      </CDropdownToggle>
      <CDropdownMenu className="pt-0" placement="bottom-end">
        <CDropdownHeader className="bg-light fw-semibold py-2">Cuenta</CDropdownHeader>
        <CDropdownItem href="/#/Message">
          <CIcon icon={cilEnvelopeOpen} className="me-2" />
          Mensajes
        </CDropdownItem>
        <CDropdownHeader className="bg-light fw-semibold py-2">Opciones</CDropdownHeader>
        <CDropdownItem href="/#/MyPosts">
          <CIcon icon={cilExternalLink} className="me-2" />
          Mis Publicaciones
        </CDropdownItem>
        <CDropdownItem href="/#/MyOrders">
          <CIcon icon={cilMoney} className="me-2" />
          Mis Compras
        </CDropdownItem>
        <CDropdownItem href="/#/MySales">
          <CIcon icon={cilCreditCard} className="me-2" />
          Mis Ventas
          <CBadge color="secondary" className="ms-2">
          </CBadge>
        </CDropdownItem>
        <CDropdownDivider />
        <CDropdownItem href="/#/login">
          <CIcon icon={cilLockLocked} className="me-2" />
         Cerrar Sesión
        </CDropdownItem>
      </CDropdownMenu>
    </CDropdown>
  )
}

export default AppHeaderDropdown
