// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

// CS1591 - Missing XML comment for publicly visible type or member 'Type_or_Member' is disabled in this file because it does not ship anymore.
#pragma warning disable 1591

namespace System.Fabric.Testability.Client.Requests
{
    using System;
    using System.Fabric.Description;
    using System.Fabric.Interop;
    using System.Fabric.Testability.Client.Structures;
    using System.Fabric.Testability.Common;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpgradeApplicationRequest : FabricRequest
    {
        public UpgradeApplicationRequest(IFabricClient fabricClient, ApplicationUpgradeDescription upgradeDescription, TimeSpan timeout)
            : base(fabricClient, timeout)
        {
            ThrowIf.Null(upgradeDescription, "upgradeDescription");

            this.UpgradeDescription = upgradeDescription;
            this.ConfigureErrorCodes();
        }

        public ApplicationUpgradeDescription UpgradeDescription
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Upgrade application {0} with timeout {1}", this.UpgradeDescription.ApplicationName, this.Timeout);
        }

        public override async Task PerformCoreAsync(CancellationToken cancellationToken)
        {
            this.OperationResult = await this.FabricClient.UpgradeApplicationAsync(this.UpgradeDescription, this.Timeout, cancellationToken);
        }

        private void ConfigureErrorCodes()
        {
            this.SucceedErrorCodes.Add((uint)NativeTypes.FABRIC_ERROR_CODE.FABRIC_E_APPLICATION_UPGRADE_IN_PROGRESS);
            this.SucceedErrorCodes.Add((uint)NativeTypes.FABRIC_ERROR_CODE.FABRIC_E_APPLICATION_ALREADY_IN_TARGET_VERSION);
        }
    }
}


#pragma warning restore 1591