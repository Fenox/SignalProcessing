<?xml version="1.0" encoding="utf-8"?>
<!--
/* //device/apps/common/assets/res/layout/list_content.xml
**
** Copyright 2006, The Android Open Source Project
**
** Licensed under the Apache License, Version 2.0 (the "License");
** you may not use this file except in compliance with the License.
** You may obtain a copy of the License at
**
**     http://www.apache.org/licenses/LICENSE-2.0
**
** Unless required by applicable law or agreed to in writing, software
** distributed under the License is distributed on an "AS IS" BASIS,
** WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
** See the License for the specific language governing permissions and
** limitations under the License.
*/
-->

<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_height="match_parent"
    android:layout_width="match_parent">

    <LinearLayout
        android:orientation="horizontal"
        android:layout_width="match_parent"
        android:layout_height="0px"
        android:layout_weight="1">

        <LinearLayout
            style="?attr/preferenceHeaderPanelStyle"
            android:id="@+id/headers"
            android:orientation="vertical"
            android:layout_width="0px"
            android:layout_height="match_parent"
            android:layout_weight="@integer/preferences_left_pane_weight">

            <ListView android:id="@android:id/list"
                style="?attr/preferenceListStyle"
                android:layout_width="match_parent"
                android:layout_height="0px"
                android:layout_weight="1"
                android:clipToPadding="false"
                android:drawSelectorOnTop="false"
                android:cacheColorHint="@android:color/transparent"
                android:listPreferredItemHeight="48dp"
                android:scrollbarAlwaysDrawVerticalTrack="true" />

            <FrameLayout android:id="@+id/list_footer"
                    android:layout_width="match_parent"
                    android:layout_height="wrap_content"
                    android:layout_weight="0" />

        </LinearLayout>

        <LinearLayout
                android:id="@+id/prefs_frame"
                style="?attr/preferencePanelStyle"
                android:layout_width="0px"
                android:layout_height="match_parent"
                android:layout_weight="@integer/preferences_right_pane_weight"
                android:orientation="vertical"
                android:visibility="gone" >

            <!-- Breadcrumb inserted here, in certain screen sizes. In others, it will be an
                empty layout or just padding, and PreferenceActivity will put the breadcrumbs in
                the action bar. -->
            <include layout="@layout/breadcrumbs_in_fragment" />

            <android.preference.PreferenceFrameLayout android:id="@+id/prefs"
                    android:layout_width="match_parent"
                    android:layout_height="0dip"
                    android:layout_weight="1"
                />
        </LinearLayout>
    </LinearLayout>

    <RelativeLayout android:id="@+id/button_bar"
        android:layout_height="wrap_content"
        android:layout_width="match_parent"
			android:layout_weight="0"
        android:visibility="gone">

        <Button android:id="@+id/back_button"
            android:layout_width="150dip"
            android:layout_height="wrap_content"
            android:layout_margin="5dip"
            android:layout_alignParentStart="true"
            android:text="@string/back_button_label"
        />
        <LinearLayout
            android:orientation="horizontal"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:layout_alignParentEnd="true">

            <Button android:id="@+id/skip_button"
                android:layout_width="150dip"
                android:layout_height="wrap_content"
                android:layout_margin="5dip"
                android:text="@string/skip_button_label"
                android:visibility="gone"
            />

            <Button android:id="@+id/next_button"
                android:layout_width="150dip"
                android:layout_height="wrap_content"
                android:layout_margin="5dip"
                android:text="@string/next_button_label"
            />
        </LinearLayout>
    </RelativeLayout>
</LinearLayout>
