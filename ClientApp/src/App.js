import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import GetCustomerItems from './components/GetCustomerItems';

export default () => (
  <Layout>
    <Route exact path='/:customerId?' component={GetCustomerItems} />
  </Layout>
);
