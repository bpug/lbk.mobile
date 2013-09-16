//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IDocumentViewer.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.DocumentViewer
{
    public interface IDocumentViewerTask
    {
        void ShowPdf(string localPath, string url, bool onTop = false);
    }
}