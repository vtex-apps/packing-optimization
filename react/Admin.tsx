import type { FC } from "react";
import React from "react";
import { useIntl } from "react-intl";
import {
  Page,
  PageHeader,
  PageTitle,
  createSystem,
  ToastProvider,
} from "@vtex/admin-ui";

const Admin: FC = () => {
  const [ThemeProvider] = createSystem({
    key: "packing-optimization",
  });

  const { formatMessage } = useIntl();

  return (
    <ThemeProvider>
      <ToastProvider>
        <Page className="pa7">
          <PageHeader>
            <PageTitle>
              {formatMessage({ id: "admin/packing-optimization.title" })}
            </PageTitle>
          </PageHeader>
        </Page>
      </ToastProvider>
    </ThemeProvider>
  );
};

export default Admin;
