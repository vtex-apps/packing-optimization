/* eslint-disable @typescript-eslint/no-explicit-any */
import { PageContent } from '@vtex/admin-ui'
import type { FC } from 'react'
import React, { useState } from 'react'

const BoxConfigurations: FC = () => {
  const [state] = useState<{
    containers: Container[]
  }>({
    containers: [],
  })

  const { containers } = state

  return <PageContent>{containers}</PageContent>
}

export default BoxConfigurations
