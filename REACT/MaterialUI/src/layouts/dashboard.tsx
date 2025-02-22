import { DashboardLayout, PageContainer } from '@toolpad/core';
import { Outlet } from 'react-router';

export default function Dashboard() {
  return (
    <DashboardLayout>
      <PageContainer>
        <Outlet />
      </PageContainer>
    </DashboardLayout>
  );
}