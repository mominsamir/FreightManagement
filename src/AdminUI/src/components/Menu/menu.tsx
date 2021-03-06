import React from 'react';
import { Menu as AntMenu } from 'antd';
import { Link } from 'react-router-dom';
import styles from './menu.module.less';
const AntSubMenu = AntMenu.SubMenu;

interface Props {
  menu: Array<Menu>;
}

export interface Menu {
  item: MenuItem;
  children?: Array<MenuItem>;
}

export interface MenuItem {
  label: string;
  key: string;
  url?: string;
  icon?: string;
}

const MenuComponent: React.FunctionComponent<Props> = ({ menu }) => {
  const pathKeys = window.location.pathname.split('/').filter((p) => p !== '');

  const renderSubMenu = (subMenu: MenuItem, children?: Array<MenuItem>) => {
    return (
      <AntSubMenu
        key={subMenu.key}
        title={
          <span>
            <span className="nav-text">{subMenu.label}</span>
          </span>
        }
      >
        {children && children.map((c) => renderMenuItem(c))}
      </AntSubMenu>
    );
  };

  const renderMenuItem = (menuItem: MenuItem) => (
        <AntMenu.Item key={menuItem.key}>
          <Link to={menuItem.url || ''}>
            <span className="nav-text">{menuItem.label}</span>
          </Link>
        </AntMenu.Item>
    );

  const renderMenu = () => menu.map((item) => item.children && item.children.length > 0 ? renderSubMenu(item.item, item.children) : renderMenuItem(item.item));
  

  return (
    <AntMenu
      theme="dark"
      className={styles.menu}
      mode="horizontal"
      defaultSelectedKeys={pathKeys.length === 0 ? ['home'] : pathKeys}
    >
      {renderMenu()}
    </AntMenu>
  );
};

export default MenuComponent;
