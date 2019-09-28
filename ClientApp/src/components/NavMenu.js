import React from 'react';
import { Container, Navbar, NavbarBrand, NavbarToggler } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export default class NavMenu extends React.Component {
  constructor (props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false
    };
  }
  toggle () {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  render () {
      return (
          <header>
              <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light >
                  <Container>
                      <NavbarBrand tag={Link} to="/"><img src="./images/ProfilePhoto.jpg" alt="Glen Liu" className="rounded-circle" /><span> Nude Assignment</span></NavbarBrand>
                      <NavbarToggler onClick={this.toggle} className="mr-2" />
                  </Container>
              </Navbar>
          </header>
      );
  }
}
